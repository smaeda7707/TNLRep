using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class GrimChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 7;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.10f;
            player.GetKnockback(DamageClass.Generic) += 0.08f;
        }
    }

}
