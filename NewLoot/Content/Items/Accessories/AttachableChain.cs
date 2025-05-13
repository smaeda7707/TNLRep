using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;

namespace NewLoot.Content.Items.Accessories
{
    public class AttachableChain : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 0, 50);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AttachableChainPlayer>().attachableChainEquipped = true;
        }

    }
    public class AttachableChainPlayer : ModPlayer
    {
        public bool attachableChainEquipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            attachableChainEquipped = false;

        }
    }
}
