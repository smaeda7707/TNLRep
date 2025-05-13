using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class MagtaneBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagtaneBar>(), 15);
            recipe.AddIngredient(ItemID.Topaz, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
