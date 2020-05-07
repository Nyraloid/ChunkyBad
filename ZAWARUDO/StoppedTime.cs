using Terraria;
using Terraria.ModLoader;
using Chunky.NPCs;

namespace Chunky.ZAWARUDO
{
    public class StoppedTime : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Stopped Time");
            Description.SetDefault("TOKI WO TOMARE");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = false;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ChunkyPlayer>().tsImmune = true;
        }
    }
}