using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class LeadGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.LeadHelmet, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.LeadChainmail, out Item result2))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.LeadGreaves, out Item result3))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.LeadBroadsword, out Item result4))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.LeadShortsword, out Item result5))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.LeadBow, out Item result6))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
