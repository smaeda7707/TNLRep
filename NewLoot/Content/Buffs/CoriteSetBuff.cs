using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class CoriteSetBuff : ModBuff
    {
        public int timer = 0;

        public override void Update(Player player, ref int buffIndex)
        {
            player.noKnockback = true;

            if (timer%3 == 0)
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.SilverCoin, 0f, 0f, 100, default, 1f);
                dust.velocity *= 0.6f;
                dust.noGravity = true;
            }

            timer++;
        }

    }

}
