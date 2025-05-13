using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Items
{
    internal class MoltenString : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 28;
            Item.height = 28;

            Item.value = Item.buyPrice (gold: 2);
            Item.maxStack = 1; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Orange;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
    
}
