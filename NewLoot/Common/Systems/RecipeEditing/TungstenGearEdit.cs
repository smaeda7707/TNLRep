using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class TungstenGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.TungstenHelmet, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TungstenChainmail, out Item result2))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TungstenGreaves, out Item result3))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TungstenBroadsword, out Item result4))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TungstenShortsword, out Item result5))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TungstenBow, out Item result6))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.EmeraldStaff, out Item result7))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
