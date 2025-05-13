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
    [AutoloadEquip(EquipType.Shoes)] // Load the spritesheet you create as a shield for the player when it is equipped.
    public class SwiftBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 0, 90);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.runAcceleration += 0.10f;
        }

    }
}
