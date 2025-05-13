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
    public class CoolingPack : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 1, 90);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CoolingPackPlayer>().coolingPackEquipped = true;
        }

    }
    public class CoolingPackPlayer : ModPlayer
    {
        public static int timer = 0;

        // The fields related to the dash accessory
        public bool coolingPackEquipped;

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
                coolingPackEquipped = false;
            }
            return timer;
        }
    }
}
