using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class MagtaneHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.jumpSpeedBoost += 0.10f;
            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagtaneBar>(), 10);
            recipe.AddIngredient(ItemID.Topaz, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Falling speed decreased";
            player.maxFallSpeed -= 4f;
            player.extraFall += 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<MagtaneBoots>() && body.type == ModContent.ItemType<MagtaneChestplate>())
            {
                return true;
            }
            return false;
        }
    }
}
