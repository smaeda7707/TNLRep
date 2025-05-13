using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class KanumHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.06f;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KanumBar>(), 12);
            recipe.AddIngredient(ItemID.Emerald, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased damage by 12%\nMax life decreased by 40";
            player.statLifeMax2 -= 40;
            player.GetDamage(DamageClass.Generic) += 0.12f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<KanumGreaves>() && body.type == ModContent.ItemType<KanumChainmail>())
            {
                return true;
            }
            return false;
        }
    }
}
