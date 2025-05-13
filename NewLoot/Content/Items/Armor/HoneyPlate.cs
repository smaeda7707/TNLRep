using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class HoneyPlate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 7;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.06f;
            player.moveSpeed -= 0.08f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledHoney, 20);
            recipe.AddIngredient(ItemID.BeeWax, 12);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
