using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;

namespace NewLoot.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)] // Load the spritesheet you create as a shield for the player when it is equipped.
    public class FrenzyGlove : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 1, 20);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FrenzyGlovePlayer>().equipped = true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.JungleSpores, 8)
                .AddIngredient(ModContent.ItemType<DemonitePowder>(), 8)
                .AddTile(TileID.Loom)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.JungleSpores, 8)
                .AddIngredient(ModContent.ItemType<CrimtanePowder>(), 8)
                .AddTile(TileID.Loom)
                .Register();
        }
    }
    public class FrenzyGlovePlayer : ModPlayer
    {
        public bool equipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            equipped = false;

        }
    }
}
