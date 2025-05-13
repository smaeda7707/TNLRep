using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using System.Threading;

namespace NewLoot.Content.Items.Accessories
{
    public class DesertLamp : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 90);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DesertLampPlayer>().desertLampEquipped = true;
        }

    }
    public class DesertLampPlayer : ModPlayer
    {
        public static int timer = 0;

        // The fields related to the dash accessory
        public bool desertLampEquipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            MakeSureEquipWork();
        }
        public int MakeSureEquipWork()
        {
            if (timer == 0)
            {
                timer++;
            }
            else
            {
                timer = 0;
                desertLampEquipped = false;
            }
            return timer;
        }
    }
}
