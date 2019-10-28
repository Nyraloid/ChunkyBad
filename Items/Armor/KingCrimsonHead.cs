using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Chunky.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class KingCrimsonHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King's Mask");
            Tooltip.SetDefault("5% damage reduction\nLife and mana regeneration increased by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 10000;
            item.rare = 9;
            item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("KingCrimsonBody") && legs.type == mod.ItemType("KingCrimsonLegs");
        }


        public override void UpdateEquip(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            player.endurance += 0.05f;
            player.lifeRegen += 1;
            player.manaRegen += 1;
            Vector2 position = player.position;
            Lighting.AddLight((int)((position.X + (float)(player.width / 2)) / 16f), (int)((position.Y + (float)(player.height / 2)) / 16f), 1f, 0.9f, 0.4f);
        }

        public override void UpdateArmorSet(Player player)
        {
            ChunkyPlayer modPlayer = player.GetModPlayer<ChunkyPlayer>();
            player.setBonus = "While equipped with the Magic watch, Pressing the 'Skip Time' button will now allow you\nto see through all enemy movements and erase time.";

            //Unskipping time
            if (Chunky.SkipTime.JustPressed && ChunkyWorld.TimeSkipping > 0 && modPlayer.CanUseTimeMagic)
            {
                int t0 = modPlayer.kcCoolDown;
                int t = modPlayer.kcDuration;
                int x = t - ChunkyWorld.TimeSkipping;

                player.ClearBuff(mod.BuffType("TimeLeap"));
                player.ClearBuff(mod.BuffType("OutOfTime"));
                player.AddBuff(mod.BuffType("OutOfTime"), (int)((t0 - t) * (1 - Math.Cos(x * Math.PI / t)) / 2), true);
                ChunkyWorld.TimeSkipping = 0;
            }

            //Skipping time
            if (Chunky.SkipTime.JustPressed && modPlayer.kcChillBro == false && ChunkyWorld.TOKIWOTOMARE == 0 && modPlayer.CanUseTimeMagic)
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/ReverseFart"));
                ChunkyWorld.TimeSkipping = modPlayer.kcDuration;
                player.AddBuff(mod.BuffType("OutOfTime"), modPlayer.kcCoolDown, true);
                player.AddBuff(mod.BuffType("TimeLeap"), modPlayer.kcDuration, true);
            }

            if (ChunkyWorld.TimeSkipping > 0 && modPlayer.kcImmune == true)
            {
                player.moveSpeed += 7f;
                player.maxRunSpeed += 7f;
                player.jumpSpeedBoost += 5f;
                player.immune = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "InfestedMeteorite", 10);
            recipe.AddIngredient(ItemID.ShroomiteBar, 7);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.BurningHadesDye, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}