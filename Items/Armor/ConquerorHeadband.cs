using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Chunky.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ConquerorHeadband : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doubled damage\n+100 max mana and health, and 30% less mana usage\n+1 second of stopped time\nDangersense, hunter, and spelunker potion effects");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 1000000;
            item.rare = 11;
            item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ConquerorShirt") && legs.type == mod.ItemType("ConquerorPants");
        }


        public override void UpdateEquip(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            player.meleeDamage *= 2;
            player.magicDamage *= 2;
            player.rangedDamage *= 2;
            player.minionDamage *= 2;
            player.statManaMax2 += 100;
            player.statLifeMax2 += 100;
            player.manaCost -= 0.3f;
            modPlayer.tsDuration += 60;
            player.dangerSense = true;
            player.detectCreature = true;
            player.findTreasure = true;
            Vector2 position = player.position;
            Lighting.AddLight((int)((position.X + (float)(player.width / 2)) / 16f), (int)((position.Y + (float)(player.height / 2)) / 16f), 1.11f, .90f, .40f);
        }

        public override void UpdateArmorSet(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();

            player.setBonus = "+6 seconds of stopping time, and -6 seconds of cooldown\nYou cannot run out of mana\nImmunity to pretty much everything\nImmunity to knockback\nRegeneration increased by 10\n'You've conquerored the world, it only makes sense for you to control time as well'";

            modPlayer.tsDuration += 360;
            modPlayer.tsCoolDown -= 360;
            player.lifeRegen += 10;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.noKnockback = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfestedMeteorite", 10);
            recipe.AddIngredient(null, "MagicWatch", 1);
            recipe.AddIngredient(ItemID.SolarFlareHelmet, 1);
            recipe.AddIngredient(ItemID.VortexHelmet, 1);
            recipe.AddIngredient(ItemID.NebulaHelmet, 1);
            recipe.AddIngredient(ItemID.StardustHelmet, 1);
            recipe.AddIngredient(ItemID.Sundial, 1);
            recipe.AddIngredient(ItemID.Blindfold, 1);
            recipe.AddIngredient(ItemID.BrightGreenDye, 1);
            recipe.AddIngredient(ItemID.GreenDye, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}