using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class VigilanteChest : ModItem
    {
        private int equip = -1;
        public override void SetDefaults()
        {
            Item.defense = 7;
            Item.value = 15000;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 38);
            recipe.AddIngredient(ModContent.ItemType<BoneMarrow>(), 30);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

    }
}
