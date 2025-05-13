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
    public class DesertTrinket : ModItem
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
            player.maxRunSpeed += 0.18f;
        }

    }
}
