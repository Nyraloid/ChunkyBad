using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Chunky.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ConquerorShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("16% increased damage\nIncreased invincibility time and endurance\n+1 second of stopped time\nRod of discord has no penalty while not on cooldown");
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
            player.meleeDamage += 0.16f;
            player.magicDamage += 0.16f;
            player.rangedDamage += 0.16f;
            player.minionDamage += 0.16f;
            player.longInvince = true;
            player.endurance += 0.1f;
            modPlayer.tsDuration += 60;
            Vector2 position = player.position;
            Lighting.AddLight((int)((position.X + (float)(player.width / 2)) / 16f), (int)((position.Y + (float)(player.height / 2)) / 16f), 1.11f, .90f, .40f);


            if (ChunkyWorld.TOKIWOTOMARE == 0 && !modPlayer.tsChillBro)
            {
                player.buffImmune[BuffID.ChaosState] = true;
            }
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfestedMeteorite", 10);
            recipe.AddIngredient(null, "MagicWatch", 1);
            recipe.AddIngredient(ItemID.SolarFlareBreastplate, 1);
            recipe.AddIngredient(ItemID.VortexBreastplate, 1);
            recipe.AddIngredient(ItemID.NebulaBreastplate, 1);
            recipe.AddIngredient(ItemID.StardustBreastplate, 1);
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddIngredient(ItemID.CountercurseMantra, 1);
            recipe.AddIngredient(ItemID.ArmorBracing, 1);
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