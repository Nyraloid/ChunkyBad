using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chunky.Items.Material
{
    public class SoulOfPlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Plight");
            Tooltip.SetDefault("'Essence of calamities'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.value = 3000;
            item.rare = 9;
            item.maxStack = 999;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.6f * Main.essScale);
        }
    }
}