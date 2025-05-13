using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class ScaleFossilHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 2;
            player.moveSpeed -= 0.04f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilOre, 16);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddIngredient(ItemID.Amber, 4);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
