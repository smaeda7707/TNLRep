using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class ScaleFossilPlate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 2;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilOre, 20);
            recipe.AddIngredient(ItemID.ShadowScale, 18);
            recipe.AddIngredient(ItemID.Amber, 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
