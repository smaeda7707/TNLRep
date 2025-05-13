using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class BoneMarrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 20;
            Item.height = 14;

            Item.value = Item.buyPrice (copper: 15);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 2);
            recipe.AddIngredient(ItemID.SiltBlock, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
    
}
