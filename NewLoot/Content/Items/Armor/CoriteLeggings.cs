using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class CoriteLeggings : ModItem
    {
        private int setEnergyCost = 20;
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CoriteBar>(), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to become immune to knockback for 20 seconds\nUses 20 energy";
            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] < 15 && !player.HasBuff(ModContent.BuffType<CoriteSetBuff>()) && player.GetModPlayer<Energy>().energyCurrent >= setEnergyCost)
            {
                player.AddBuff(ModContent.BuffType<CoriteSetBuff>(), 1200);
                player.GetModPlayer<Energy>().energyCurrent -= setEnergyCost;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<CoriteHelmet>() && body.type == ModContent.ItemType<CoriteChestplate>()) 
            { 
                return true;
            }
            return false;
        }
    }
}
