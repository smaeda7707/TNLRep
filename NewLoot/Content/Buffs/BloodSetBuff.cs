using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class BloodSetBuff : ModBuff
    {
        public int timer = 0;

        public override void Update(Player player, ref int buffIndex)
        {
            if (timer%4 == 0)
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.CrimsonTorch, 0f, 0f, 100, default, 2f);
                dust.velocity *= 0.3f;
                dust.noGravity = true;
            }

            timer++;
        }

    }

}
