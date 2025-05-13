using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;

namespace NewLoot.Content.Items.Accessories
{
    public class IceTrinket : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 80);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<IceTrinketPlayer>().iceTrinketEquipped = true;
        }

    }
    public class IceTrinketPlayer : ModPlayer
    {
        public bool iceTrinketEquipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            iceTrinketEquipped = false;

        }
    }
}
