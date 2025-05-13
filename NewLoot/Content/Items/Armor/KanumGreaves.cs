using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class KanumGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<KanumGreavesPlayer>().equipped = true;
            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KanumBar>(), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class KanumGreavesPlayer : ModPlayer
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
