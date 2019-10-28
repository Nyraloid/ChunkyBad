using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Chunky.NPCs;

//Big thank you to Enigma for being open source

namespace Chunky
{
    public partial class ChunkyPlayer : ModPlayer
    {
        //ZA WARUDO
        public bool tsImmune = false;
        public bool tsChillBro = false;
        public int tsCoolDown = 0;
        public int tsDuration = 0;
        public float xP = 0;
        public float yP = 0;

        //King Crimson
        public bool kcImmune = false;
        public bool kcChillBro = false;
        public int kcCoolDown = 0;
        public int kcDuration = 0;
        public float xV = 0;
        public float yV = 0;

        //Other
        public bool CanUseTimeMagic = false;

        public override void Initialize()
        {
            
        }

        public override void ResetEffects()
        {
            CanUseTimeMagic = false;

            //ZA WARUDO
            tsCoolDown = 2000;
            tsDuration = 60;
            tsImmune = false;
            tsChillBro = false;

            //King Crimson
            kcImmune = false;
            kcChillBro = false;
            kcCoolDown = 2000;
            kcDuration = 360;

            if (Main.expertMode)
            {
                tsDuration += (int)(tsDuration / 4);
                kcDuration += (int)(kcDuration / 4);
            }

            if (player.statLife <= player.statLife / 2) tsCoolDown += (int)(tsCoolDown * player.statLife / (player.statLifeMax2 * 3));
            if (NPC.downedFishron)
            {
                tsDuration += 30;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 20;
            }
            if (NPC.downedAncientCultist)
            {
                tsDuration += 30;
                tsCoolDown -= 60;
                kcDuration += 30;
            }
            if (NPC.downedTowerSolar)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 15;
                kcCoolDown -= 15;
            }
            if (NPC.downedTowerVortex)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 15;
                kcCoolDown -= 15;
            }
            if (NPC.downedTowerNebula)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 15;
                kcCoolDown -= 15;
            }
            if (NPC.downedTowerStardust)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 15;
                kcCoolDown -= 15;
            }
            if (NPC.downedMoonlord)
            {
                tsDuration += 120;
                tsCoolDown -= 60;
                kcDuration += 120;
            }
        }

        public override void PostUpdate()
        {
            PostUpdateTimeStop();
            PostUpdateTimeSkip();
        }

        private void PostUpdateTimeStop()
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0 && tsImmune == false)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                if (xP == 0 && yP == 0)
                {
                    xP = player.position.X;
                    yP = player.position.Y;
                }
                else
                {
                    player.position.X = xP;
                    player.position.Y = yP;
                }
            }
            else
            {
                xP = 0;
                yP = 0;
            }
        }

        private void PostUpdateTimeSkip()
        {
            if (ChunkyWorld.TimeSkipping > 0 && kcImmune == false)
            {
                if (xV == 0 && yV == 0)
                {
                    xV = player.velocity.X;
                    yV = player.velocity.Y;
                }
                else
                {
                    player.velocity.X = xV / 2;
                    player.velocity.Y = yV / 2;
                }
            }
            else
            {
                xV = 0;
                yV = 0;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0 && !tsImmune)
            {
                player.lifeRegenTime = 0;
                player.lifeRegen = 0;
            }
            if (ChunkyWorld.TimeSkipping > 0) player.lifeRegen -= player.lifeRegen / 2;
        }

        public override bool PreItemCheck()
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0 && tsImmune == false) return false;
            if (ChunkyWorld.TimeSkipping > 0 && kcImmune == false) return false;
            return true;
        }

        //Hotkey
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            //Resuming time
            if (Chunky.StopTime.JustPressed && ChunkyWorld.TOKIWOTOMARE > 0 && CanUseTimeMagic && Main.hardMode)
            {
                int t0 = tsCoolDown;
                int t = tsDuration;
                int x = t - ChunkyWorld.TOKIWOTOMARE;

                player.ClearBuff(mod.BuffType("StoppedTime"));
                player.ClearBuff(mod.BuffType("OutOfTime"));
                player.AddBuff(mod.BuffType("OutOfTime"), (int)((t0 - t) * (1 - Math.Cos(x * Math.PI / t)) / 2), true);
                ChunkyWorld.TOKIWOTOMARE = 0;
            }

            //Stopping time
            if (Chunky.StopTime.JustPressed && tsChillBro == false && ChunkyWorld.TimeSkipping == 0 && CanUseTimeMagic)
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/DubstepFarts"));
                ChunkyWorld.TOKIWOTOMARE = tsDuration;
                player.AddBuff(mod.BuffType("OutOfTime"), tsCoolDown, true);
                player.AddBuff(mod.BuffType("StoppedTime"), tsDuration, true);
            }
        }

        public override void UpdateBiomeVisuals()
        {
            bool stopTime = false;
            bool skipTime = false;
            if (ChunkyWorld.TOKIWOTOMARE > 0) stopTime = true;
            if (tsImmune == true) player.ManageSpecialBiomeVisuals("Chunky:ZaWarudo", stopTime); //Stopping Time visuals
            if (ChunkyWorld.TimeSkipping > 0) skipTime = true;
            if (kcImmune == true) player.ManageSpecialBiomeVisuals("Chunky:KingCrimson", skipTime); //Skipping time visuals

            //Removing the visuals
            if (ChunkyWorld.TOKIWOTOMARE > 0 && Chunky.StopTime.JustPressed) player.ManageSpecialBiomeVisuals("Chunky:ZaWarudo", false);
            if (ChunkyWorld.TimeSkipping > 0 && Chunky.SkipTime.JustPressed) player.ManageSpecialBiomeVisuals("Chunky:KingCrimson", false);
        }

        public override void SetupStartInventory(IList<Item> items)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("Notification"));
            item.stack = 1;
            items.Add(item);
        }
    }
}