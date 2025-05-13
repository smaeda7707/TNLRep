using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using static NewLoot.Content.Items.Armor.MythrilHelm;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class MythrilChest : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 16;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaRegen += 2;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.moveSpeed -= 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 24);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
