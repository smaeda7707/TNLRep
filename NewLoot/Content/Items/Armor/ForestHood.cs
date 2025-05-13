using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class ForestHood : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 0.06f;
            player.moveSpeed -= 0.02f;
        }
    }
}
