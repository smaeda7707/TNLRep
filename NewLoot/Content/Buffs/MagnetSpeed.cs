using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class MagnetSpeed : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 1f;
        }

    }
}
