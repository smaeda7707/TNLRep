using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class CobaltPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 8;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
            player.moveSpeed += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 18);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
