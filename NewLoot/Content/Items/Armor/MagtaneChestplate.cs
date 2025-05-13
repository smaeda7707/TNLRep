using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class MagtaneChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagtaneBar>(), 20);
            recipe.AddIngredient(ItemID.Topaz, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
