using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class CoriteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 30;
            Item.height = 24;

            Item.value = Item.buyPrice (silver: 4);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.White;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CoriteBarTile>());
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); // Creates Recipe
            recipe.AddIngredient(ModContent.ItemType<CoriteOre>(), 4);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
    
}
