using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class ForestChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.runAcceleration += 0.07f;
            player.moveSpeed -= 0.04f;
        }
    }
}
