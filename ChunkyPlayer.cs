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

        //Cherry
        public bool CanEatCherry = true;
        public int CherryImuneTime = 0;
        public int CherryCooldownTime = 0;

        //Other
        public bool CanUseTimeMagic = false;

        //Mandom
        private int[] t = new int[361];
        private Vector2[] SaxPos = new Vector2[361];
        private Vector2[] SaxVel = new Vector2[361];
        private int[] SaxHP = new int[361];
        private int[] SaxMana = new int[361];
        public bool CanUseTimeReverse = false;
        public static bool TimeReversed = false;

        public override void Initialize()
        {
            
        }

        public override void ResetEffects()
        {
            CanUseTimeMagic = false;

            //ZA WARUDO
            tsCoolDown = 2000;
            tsDuration = 10;
            tsImmune = false;
            tsChillBro = false;

            //King Crimson
            kcImmune = false;
            kcChillBro = false;
            kcCoolDown = 2000;
            kcDuration = 360;

            //Cherry
            CanEatCherry = true;
            CherryImuneTime = 240;
            CherryCooldownTime = 2700;

            if (Main.expertMode)
            {
                tsDuration += (int)(tsDuration / 5);
                kcDuration += (int)(kcDuration / 5);
            }

            if (player.statLife <= player.statLife / 2) tsCoolDown += (int)(tsCoolDown * player.statLife / (player.statLifeMax2 * 3));
            if (NPC.downedQueenBee) { tsDuration += 30; tsCoolDown += 10; }
            if (NPC.downedBoss3) tsDuration += 20;
            if (NPC.downedMechBoss1) tsDuration += 20;
            if (NPC.downedMechBoss2) tsDuration += 20;
            if (NPC.downedMechBoss3) tsDuration += 20;
            if (NPC.downedMechBoss3 && NPC.downedMechBoss2 && NPC.downedMechBoss1) { tsCoolDown += 60; tsDuration += 10; }
            if (NPC.downedFishron)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 20;
                CherryImuneTime += 120;
                CherryCooldownTime -= 240;
            }
            if (NPC.downedAncientCultist)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                CherryImuneTime += 60;
                CherryCooldownTime -= 180;
            }
            if (NPC.downedTowerSolar)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 15;
                CherryImuneTime += 30;
                CherryCooldownTime -= 60;
            }
            if (NPC.downedTowerVortex)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 15;
                CherryImuneTime += 30;
                CherryCooldownTime -= 60;
            }
            if (NPC.downedTowerNebula)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 15;
                CherryImuneTime += 30;
                CherryCooldownTime -= 60;
            }
            if (NPC.downedTowerStardust)
            {
                tsDuration += 15;
                tsCoolDown -= 60;
                kcDuration += 30;
                kcCoolDown -= 15;
                CherryImuneTime += 30;
                CherryCooldownTime -= 60;
            }
            if (NPC.downedMoonlord)
            {
                tsDuration += 120;
                tsCoolDown -= 60;
                kcDuration += 180;
                CherryImuneTime += 600;
                CherryCooldownTime -= 360;
            }
        }

        public override void PostUpdate()
        {
            PostUpdateTimeStop();
            PostUpdateTimeSkip();

            //Mandom
            PostUpdateReverseTime();
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
                player.moveSpeed *= 2;
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
                    player.velocity.X = xV * 0.75f;
                    player.velocity.Y = yV * 0.75f;
                }
            }
            else
            {
                xV = 0;
                yV = 0;
                player.moveSpeed *= 2;
            }
        }

        //Mandom
        private void PostUpdateReverseTime()
        {
            if (ChunkyWorld.TOKIWOTOMARE == 0)
            {
                for (int i = 0; i < 360; i++) t[i] = t[i + 1]; t[360] = Convert.ToInt32(Main.time); //We need this to make sure 6 seconds have already passed in the world

                for (int i = 0; i < 360; i++)
                {
                    SaxPos[i] = SaxPos[i + 1];
                    SaxVel[i] = SaxVel[i + 1];
                    SaxHP[i] = SaxHP[i + 1];
                    SaxMana[i] = SaxMana[i + 1];
                }
                SaxPos[360] = player.position;
                SaxVel[360] = player.velocity;
                SaxHP[360] = player.statLife;
                SaxMana[360] = player.statMana;

                TimeReversed = false;

                if (Chunky.ReverseTime.JustPressed && !tsChillBro  && CanUseTimeMagic && t[0] != t[1])
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/TimeLeap")); //That's the steins;gate time leap sound
                    player.position = SaxPos[0];
                    player.velocity = SaxVel[0];
                    player.statLife = SaxHP[0];
                    player.statMana = SaxMana[0];
                    player.AddBuff(mod.BuffType("OutOfTime"), 360, true);
                    Main.time -= 360;

                    TimeReversed = true;
                }
            }
        }

        public override void UpdateBadLifeRegen() //no regen in stopped time or skipped time >:(
        {
            if (ChunkyWorld.TOKIWOTOMARE > 0 && !tsImmune || (ChunkyWorld.TimeSkipping > 0))
            {
                player.lifeRegenTime = 0;
                player.lifeRegen = 0;
            }
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
            //Making sure resuming time isn't too strong
            if (tsCoolDown < tsDuration + 360) tsCoolDown = tsDuration + 360;

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

        [Obsolete]
        public override void SetupStartInventory(IList<Item> items)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("Notification"));
            item.stack = 1;
            items.Add(item);
        }

        //Fishy Fishy
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk) return;
            //A lot of worms
            if ((Main.bloodMoon || player.ZoneCrimson) && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("RedWorm");
            if (player.ZoneUnderworldHeight && liquidType == 1 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("OrangeWorm");
            if (player.ZoneDesert && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("YellowWorm");
            if (player.ZoneJungle && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("LimeWorm");
            if (player.ZoneDirtLayerHeight && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("GreenWorm");
            if (player.ZoneHoly && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("LightBlueWorm");
            if (player.ZoneSkyHeight && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("BlueWorm");
            if ((Main.bloodMoon || player.ZoneCorrupt) && liquidType == 0 && Main.rand.Next(500) == 0) caughtType = mod.ItemType("VioletWorm");
        }
    }
}