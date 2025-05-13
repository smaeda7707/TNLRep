using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class CrimtanePowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 22;
            Item.height = 26;

            Item.value = Item.buyPrice (silver: 4);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); // Creates Recipe
            recipe.AddIngredient(ItemID.CrimtaneOre, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
    
}
