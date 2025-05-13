using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class CrimtaneGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.CrimsonHelmet, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.CrimsonScalemail, out Item result2))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.CrimsonGreaves, out Item result3))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.BloodButcherer, out Item result4))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TheMeatball, out Item result5))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.TendonBow, out Item result6))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.CrimsonYoyo, out Item result7))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
