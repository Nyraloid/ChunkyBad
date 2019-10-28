using Terraria;
using Terraria.ModLoader;
using Chunky;

namespace Chunky.ZAWARUDO
{
    public class OutOfTime : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Out of Time");
            Description.SetDefault("Woah there, you've gotta wait a bit before you can mess with time again");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ChunkyPlayer>().tsChillBro = true;
            player.GetModPlayer<ChunkyPlayer>().kcChillBro = true;
        }
    }
}