using Terraria;
using Terraria.ModLoader;

namespace Chunky.Buffs
{
    public class NoPain : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cherry Power");
            Description.SetDefault("Lelolelolelolelo");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.immune = true;
        }
    }
}