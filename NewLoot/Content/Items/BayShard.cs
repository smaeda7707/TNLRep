using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class BayShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 28;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 18;
            Item.height = 18;

            Item.value = Item.buyPrice (silver: 4);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Blue;
        }
    }
    
}
