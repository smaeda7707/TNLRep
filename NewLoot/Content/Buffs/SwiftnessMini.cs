﻿using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class SwiftnessMini : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.15f;
        }

    }
}
