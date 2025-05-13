using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class CobaltShirt : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 10;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.04f;
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 24);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
