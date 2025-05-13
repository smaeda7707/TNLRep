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
    public class ManaSyphon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 90);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ManaSyphonPlayer>().equipped = true;
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Glass, 20)
                .AddIngredient(ItemID.TissueSample, 10)
                .AddIngredient(ItemID.ManaCrystal)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

    }
    public class ManaSyphonPlayer : ModPlayer
    {
        public static int timer = 0;

        // The fields related to the dash accessory
        public bool equipped;

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
