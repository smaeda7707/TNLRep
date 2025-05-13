using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Buffs;
using Terraria.Audio;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class ForestBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.06f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased movement speed by 12%\nDecreased acceleration by 9%";
            player.moveSpeed += 0.12f;
            player.runAcceleration -= 0.09f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<ForestHood>() && body.type == ModContent.ItemType<ForestChestplate>())
            {
                return true;
            }
            return false;
        }
    }
}
