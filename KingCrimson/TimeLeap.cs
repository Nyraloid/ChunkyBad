using Terraria;
using Terraria.ModLoader;
using Chunky.NPCs;

namespace Chunky.KingCrimson
{
    public class TimeLeap : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Skipping Time");
            Description.SetDefault("It Just Works");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ChunkyPlayer>().kcImmune = true;
        }
    }
}