using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Chunky.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ConquerorPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased movement, falling, and jump speed\n12% increased damage\n+1 second of stopped time\nWater walking and immunity to lava, 'On Fire', and 'Burning'");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 1000000;
            item.rare = 11;
            item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            player.meleeDamage += 0.12f;
            player.magicDamage += 0.12f;
            player.rangedDamage += 0.12f;
            player.minionDamage += 0.12f;
            modPlayer.tsDuration += 60;
            player.buffImmune[BuffID.OnFire] = true;
            player.lavaImmune = true;
            player.waterWalk = true;
            player.buffImmune[BuffID.Burning] = true;
            player.moveSpeed += 4.5f;
            player.maxFallSpeed += 28;
            player.maxRunSpeed += 4.5f;
            player.jumpSpeedBoost += 4.5f;
            Vector2 position = player.position;
            Lighting.AddLight((int)((position.X + (float)(player.width / 2)) / 16f), (int)((position.Y + (float)(player.height / 2)) / 16f), 1.11f, .90f, .40f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfestedMeteorite", 10);
            recipe.AddIngredient(null, "MagicWatch", 1);
            recipe.AddIngredient(ItemID.SolarFlareLeggings, 1);
            recipe.AddIngredient(ItemID.VortexLeggings, 1);
            recipe.AddIngredient(ItemID.NebulaLeggings, 1);
            recipe.AddIngredient(ItemID.StardustLeggings, 1);
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddIngredient(ItemID.ThePlan, 1);
            recipe.AddIngredient(ItemID.MedicatedBandage, 1);
            recipe.AddIngredient(ItemID.BrightGreenDye, 1);
            recipe.AddIngredient(ItemID.GreenDye, 1);
            recipe.AddIngredient(ItemID.BlackDye, 1);
            recipe.AddIngredient(ItemID.YellowDye, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}