using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class RustyBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 22;
            Item.height = 26;

            Item.value = Item.buyPrice (silver: 1);
            Item.maxStack = 1; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Gray;
        }
    }
    
}
