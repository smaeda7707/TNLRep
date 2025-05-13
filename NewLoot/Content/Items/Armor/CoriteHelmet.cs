using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class CoriteHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.08f;
            player.endurance += 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CoriteBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
