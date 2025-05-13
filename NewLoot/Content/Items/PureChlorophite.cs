using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class PureChlorophite : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 26;
            Item.height = 26;

            Item.value = Item.buyPrice (silver: 85);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Lime;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); // Creates Recipe
            recipe.AddIngredient(ItemID.ChlorophyteOre, 4);
            recipe.AddIngredient(ItemID.PurificationPowder, 2);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
    
}
