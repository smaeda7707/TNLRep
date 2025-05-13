using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class HemoshroomChest : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.10f;
            player.GetDamage(DamageClass.SummonMeleeSpeed) -= 0.10f;
            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimtanePowder>(), 24);
            recipe.AddIngredient(ItemID.TissueSample, 18);
            recipe.AddIngredient(ItemID.GlowingMushroom, 16);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
