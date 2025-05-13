using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Items
{
    internal class SnatcherJaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 22;
            Item.height = 20;

            Item.value = Item.buyPrice (silver: 40);
            Item.maxStack = 1; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.White;
        }

    }
    
}
