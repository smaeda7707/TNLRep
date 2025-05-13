using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class CoriteChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.10f;
            player.statLifeMax2 += 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CoriteBar>(), 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
