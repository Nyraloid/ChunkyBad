using Terraria;
using Terraria.ModLoader;

namespace Chunky.Buffs
{
    public class YesPain : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Dropped Cherry");
            Description.SetDefault("Whoops");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ChunkyPlayer>().CanEatCherry = false;
        }
    }
}