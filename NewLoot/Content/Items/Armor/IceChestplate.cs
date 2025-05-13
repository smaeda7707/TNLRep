using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class IceChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 7;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            if (player.statLife >= player.statLifeMax)
            {
                player.maxFallSpeed += 3.2f;
                player.extraFall -= 1;
            }
            else
            {
                player.runAcceleration += 0.07f;
            }

            player.moveSpeed -= 0.09f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 16);
            recipe.AddIngredient(ItemID.IceBlock, 120);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
