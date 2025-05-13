using Microsoft.Xna.Framework;
using NewLoot.Content.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Newloot.Content.Items.Potions
{
    public class IronskinMiniPot : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(230, 222, 10),
                new Color(255, 252, 159),
                new Color(137, 133, 13)
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
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 4);
            Item.buffType = ModContent.BuffType<IronskinMini>(); // Specify an existing buff to be applied when used.
            Item.buffTime = 28800;
        }
    }
}
