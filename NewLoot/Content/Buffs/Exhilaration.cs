using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class Exhilaration : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
        }

    }
}
