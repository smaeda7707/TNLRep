using Newloot.Content.Items.Weapons;
using NewLoot.Content.Buffs;
using NewLoot.Content.Items;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Items.Weapons;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewLoot.Common.Global
{
    internal class UltimateResetGlobal : GlobalItem
    {
        private static int timer = 0;
        private static int timer2 = 0;
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (player.GetModPlayer<HeatWavePlayer>().HeatWaveEquipped == false)
            {
                timer++;
                if (timer > 3)
                {
                    player.ClearBuff(ModContent.BuffType<HeatWaveBuff>());
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }


            if (player.GetModPlayer<SubZeroPlayer>().SubZeroEquipped == false)
            {
                timer2++;
                if (timer2 > 3)
                {
                    player.ClearBuff(ModContent.BuffType<SubZeroBuff>());
                    timer2 = 0;
                }
            }
            else
            {
                timer2 = 0;
            }
        }
    }
}
