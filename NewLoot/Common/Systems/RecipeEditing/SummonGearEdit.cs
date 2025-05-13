using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class SummonGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.FlinxStaff, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.FlinxFurCoat, out Item result2))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
