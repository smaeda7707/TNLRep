using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class HemoshroomHat : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 2; // +1 life regen
            player.moveSpeed -= 0.02f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimtanePowder>(), 18);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddIngredient(ItemID.GlowingMushroom, 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
