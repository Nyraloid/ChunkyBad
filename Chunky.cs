using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.IO;
using System.Threading;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Chunky.ZAWARUDO;
using Chunky.KingCrimson;
using Terraria.Localization;

namespace Chunky
{

    class Chunky : Mod
    {
        internal static ModHotKey StopTime;
        internal static ModHotKey SkipTime;

        //Mandom
        internal static ModHotKey ReverseTime;

        public static Chunky instance;

        public Chunky()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        //Recipe Groups
        public override void AddRecipeGroups()
        {
            RecipeGroup groupC = new RecipeGroup(() => "Copper/Tin", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("Chunky:Copper/Tin", groupC);

            RecipeGroup groupS = new RecipeGroup(() =>"Silver/Tungsten", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("Chunky:Silver/Tungsten", groupS);

            RecipeGroup groupG = new RecipeGroup(() => "Gold/Platinum", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("Chunky:Gold/Platinum", groupG);

            RecipeGroup groupD = new RecipeGroup(() => "Demonite/Crimtane", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("Chunky:Demonite/Crimtane", groupD);

            RecipeGroup groupP = new RecipeGroup(() => "Palladium/Cobalt", new int[]
            {
                ItemID.PalladiumBar,
                ItemID.CobaltBar
            });
            RecipeGroup.RegisterGroup("Chunky:Palladium/Cobalt", groupP);

            RecipeGroup groupM = new RecipeGroup(() => "Mythril/Orichalcum", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup("Chunky:Mythril/Orichalcum", groupM);

            RecipeGroup groupA = new RecipeGroup(() => "Adamantite/Titanium", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("Chunky:Adamantite/Titanium", groupA);
        }

        //Hotkeys
        public override void Load()
        {
            instance = this;
            if (!Main.dedServ)
            {
                //Stopping time visuals
                Filters.Scene["Chunky:ZaWarudo"] = new Filter(new TsShader("FilterMiniTower").UseColor(0.5f, .5f, .5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["Chunky:ZaWarudo"] = new TsVisual();

                //Skipping time visuals
                Filters.Scene["Chunky:KingCrimson"] = new Filter(new KcShader("FilterMiniTower").UseColor(.66f, .07f, .41f).UseOpacity(.4f), EffectPriority.VeryHigh);
                SkyManager.Instance["Chunky:KingCrimson"] = new KcVisual();

                //Blackout
                Filters.Scene["Chunky:Blackout"] = new Filter(new KcShader("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(1f), EffectPriority.VeryHigh);
            }

            //Wir haben musik


            StopTime = RegisterHotKey("Stop Time", "Q");
            SkipTime = RegisterHotKey("Skip Time", "F");

            //Mandom
            ReverseTime = RegisterHotKey("Reverse Time", "Y");
        }

        public override void Unload()
        {
            instance = null;
        }
    }
}