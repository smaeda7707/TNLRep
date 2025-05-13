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
    public class ObsidianEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 2, 10);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HeldItem.DamageType == DamageClass.Melee || player.HeldItem.DamageType == DamageClass.MeleeNoSpeed)
            {
                player.statDefense += 5;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 40)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

    }
}
