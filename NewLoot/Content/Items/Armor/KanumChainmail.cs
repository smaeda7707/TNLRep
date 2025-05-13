using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class KanumChainmail : ModItem
    {
        public int equip = -1;
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 4f;
            player.moveSpeed -= 0.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KanumBar>(), 20);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.Emerald, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
