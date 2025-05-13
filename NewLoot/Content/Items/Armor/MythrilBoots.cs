using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class MythrilBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 13;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.14f;
            player.GetCritChance(DamageClass.Magic) += 7;
            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 18);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
