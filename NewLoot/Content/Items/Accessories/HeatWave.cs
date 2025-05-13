using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Accessories
{
    public class HeatWave : ModItem
    {
        private int ultimateCost; // Add our custom resource cost
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 1);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            ultimateCost = 180;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HeatWavePlayer>().HeatWaveEquipped = true;

            var Resource = player.GetModPlayer<Ultimate>();


            if (player.controlDown && player.releaseDown && Resource.ultimateCurrent >= ultimateCost)
            {
                player.AddBuff(ModContent.BuffType<HeatWaveBuff>(), 1500);
                Resource.ultimateCurrent -= ultimateCost;
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FrostCore, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class HeatWavePlayer : ModPlayer
    {

        // The fields related to the dash accessory
        public bool HeatWaveEquipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            HeatWaveEquipped = false;
        }
    }
}
