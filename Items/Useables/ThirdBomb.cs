using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Chunky.Items.Useables
{
    public class ThirdBomb : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Third Bomb");
            Tooltip.SetDefault("Teleports you to your last death location\n'Killa Queen! Daisan no Bakudan! Bites Za Dusto!");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 40;
            item.autoReuse = false;
            item.rare = 9;
            item.value = Item.sellPrice(0, 0, 50);
            item.useStyle = 4;
            item.useAnimation = 18;
            item.useTime = 45;
            item.mana = 150;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.lastDeathPostion.X >= 1 && player.lastDeathPostion.Y >= 1) return true;
            return false;
        }

        public override bool UseItem(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            if (player.lastDeathPostion.X >= 1 && player.lastDeathPostion.Y >= 1)
            {
                Vector2 LastSeen;
                LastSeen.X = player.lastDeathPostion.X - player.width;
                LastSeen.Y = player.lastDeathPostion.Y - player.height;
                player.Teleport(LastSeen, 0);
                LastSeen.X = -1;
                LastSeen.Y = -1;
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, 13);
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 6);
            recipe.AddIngredient(null, "SoulOfPlight", 5);
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
