using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Chunky.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class KingCrimsonLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King's Legs");
            Tooltip.SetDefault("Increased movement, jump, and falling speed\n+30 mana and health, and 5% less mana cost");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 10000;
            item.rare = 9;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            player.maxFallSpeed += 10;
            player.maxRunSpeed += 2;
            player.moveSpeed += 2;
            player.jumpSpeedBoost += 2;
            player.statManaMax2 += 30;
            player.statLifeMax2 += 30;
            player.manaCost -= 0.5f;
            Vector2 position = player.position;
            Lighting.AddLight((int)((position.X + (float)(player.width / 2)) / 16f), (int)((position.Y + (float)(player.height / 2)) / 16f), 0.55f, 0.05f, 0.3f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfestedMeteorite", 10);
            recipe.AddIngredient(ItemID.ShroomiteBar, 9);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.BurningHadesDye, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}