using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class HoneyHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Generic) += 0.09f;
            player.moveSpeed -= 0.06f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledHoney, 16);
            recipe.AddIngredient(ItemID.BeeWax, 8);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
