using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class IceMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            if (player.statLife >= player.statLifeMax)
            {
                player.statDefense += 1;
            }
            else
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.06f;
            }

            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddIngredient(ItemID.IceBlock, 180);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
