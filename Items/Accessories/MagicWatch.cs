using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chunky.Items.Accessories
{
    public class MagicWatch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Watch");
            Tooltip.SetDefault("'Lets you tell the time'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.value = 10000;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();

            modPlayer.CanUseTimeMagic = true;
        }

        public override void UpdateInventory(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();

            modPlayer.CanUseTimeMagic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipeP = new ModRecipe(mod);
            recipeP.AddIngredient(ItemID.PlatinumWatch, 1);
            recipeP.AddIngredient(ItemID.ManaCrystal, 2);
            recipeP.AddTile(TileID.Tables);
            recipeP.AddTile(TileID.Chairs);
            recipeP.SetResult(this);
            recipeP.AddRecipe();

            ModRecipe recipeG = new ModRecipe(mod);
            recipeG.AddIngredient(ItemID.GoldWatch, 1);
            recipeG.AddIngredient(ItemID.ManaCrystal, 2);
            recipeG.AddTile(TileID.Tables);
            recipeG.AddTile(TileID.Chairs);
            recipeG.SetResult(this);
            recipeG.AddRecipe();
        }
    }
}