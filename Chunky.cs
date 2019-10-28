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

namespace Chunky
{

    class Chunky : Mod
    {
        internal static ModHotKey StopTime;
        internal static ModHotKey SkipTime;

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

            StopTime = RegisterHotKey("Stop Time", "I");
            SkipTime = RegisterHotKey("Skip Time", "J");
        }

        public override void Unload()
        {
            instance = null;
        }
    }
}