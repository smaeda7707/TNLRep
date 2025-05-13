using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class BloodChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 7;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 16);
            recipe.AddIngredient(ItemID.SilverBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.10f;
            player.GetModPlayer<BloodChestplatePlayer>().equipped = true;
        }
    }
    public class BloodChestplatePlayer : ModPlayer
    {

        // The fields related to the dash accessory
        public bool equipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            equipped = false;
        }
    }

}
