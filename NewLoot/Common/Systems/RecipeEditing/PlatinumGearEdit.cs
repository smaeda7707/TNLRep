using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class PlatinumGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.PlatinumHelmet, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.PlatinumChainmail, out Item result2))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.PlatinumGreaves, out Item result3))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.PlatinumBroadsword, out Item result4))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.PlatinumShortsword, out Item result5))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.PlatinumBow, out Item result6))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.DiamondStaff, out Item result7))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
