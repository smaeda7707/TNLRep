using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class VigilanteCowl : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 15500;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
            player.moveSpeed -= 0.04f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 32);
            recipe.AddIngredient(ModContent.ItemType<BoneMarrow>(), 26);
            recipe.AddIngredient(ItemID.Silk, 7);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
