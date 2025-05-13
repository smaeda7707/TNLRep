using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Newloot.Content.Items.Potions;

namespace NewLoot.Common.Systems.RecipeEditing
{
    internal class PotionEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.IronskinPotion, out Item result))
                {
                    recipe.ReplaceResult(ModContent.ItemType<IronskinMiniPot>());
                }
                if (recipe.TryGetResult(ItemID.SwiftnessPotion, out Item result2))
                {
                    recipe.ReplaceResult(ModContent.ItemType<SwiftnessMiniPot>());
                }
            }
        }
    }
            
}
