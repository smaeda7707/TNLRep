using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class SilverGearEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.SilverHelmet, out Item result))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SilverChainmail, out Item result2))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SilverGreaves, out Item result3))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SilverBroadsword, out Item result4))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SilverShortsword, out Item result5))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SilverBow, out Item result6))
                {
                    recipe.DisableRecipe();
                }
                if (recipe.TryGetResult(ItemID.SapphireStaff, out Item result7))
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
            
}
