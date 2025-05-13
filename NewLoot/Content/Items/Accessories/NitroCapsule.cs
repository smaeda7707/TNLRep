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
    public class NitroCapsule : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 2, 0);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NitroCapsulePlayer>().equipped = true;
        }

    }
    public class NitroCapsulePlayer : ModPlayer
    {
        public static int timer = 0;

        // The fields related to the dash accessory
        public bool equipped;
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (equipped && item.DamageType == DamageClass.Ranged)
            {
                velocity *= 1.18f;
            }
        }
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
                equipped = false;
            }
            return timer;
        }
    }
}
