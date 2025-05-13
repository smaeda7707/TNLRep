using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class BloodHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddIngredient(ItemID.SilverBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.08f;
            player.GetArmorPenetration(DamageClass.Generic) += 1;
        }
    }
}
