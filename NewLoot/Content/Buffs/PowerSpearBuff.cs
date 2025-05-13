using NewLoot.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class PowerSpearBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            var Resource = player.GetModPlayer<Energy>();

            Resource.energyRegenRate += 10;
        }
    }
}
