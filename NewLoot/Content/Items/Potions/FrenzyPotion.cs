using Microsoft.Xna.Framework;
using NewLoot.Content.Buffs;
using NewLoot.Content.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Newloot.Content.Items.Potions
{
    public class FrenzyPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(64, 188, 13),
                new Color(131, 220, 75),
                new Color(24, 88, 31)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 2);
            Item.buffType = ModContent.BuffType<Exhilaration>(); // Specify an existing buff to be applied when used.
            Item.buffTime = 14400;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.JungleSpores, 2)
                .AddIngredient(ModContent.ItemType<DemonitePowder>(), 1)
                .AddTile(TileID.Bottles)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.JungleSpores, 2)
                .AddIngredient(ModContent.ItemType<CrimtanePowder>(), 1)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
