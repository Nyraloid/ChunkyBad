using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Chunky;

namespace Chunky
{
    public partial class ChunkyWorld : ModWorld
    {
        public static int TOKIWOTOMARE = 0;
        public static int TimeSkipping = 0;
        public static bool EveryOther = false;

        public override void Initialize()
        {

        }

        public override void PostUpdate()
        {
            if (TOKIWOTOMARE > 0) Main.time--; //Toki wo tomare
            if (TOKIWOTOMARE > 0) TOKIWOTOMARE--; //Toki wo ugoki desu
            if (TimeSkipping > 0) TimeSkipping--; //this is just to stop the time skip

            //Every other frame this will be true, though I'm not sure when that'll be useful
            EveryOther = !EveryOther;
        }
    }
}