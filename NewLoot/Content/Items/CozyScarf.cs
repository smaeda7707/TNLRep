using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class CozyScarf : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 20;
            Item.height = 14;

            Item.value = Item.buyPrice (gold: 1);
            Item.maxStack = 1; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Orange;
        }
    }
    
}
