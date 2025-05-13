using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)] // Load the spritesheet you create as a shield for the player when it is equipped.
    public class KanumShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 0, 15);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;

            Item.defense = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed -= 0.04f;
            player.AddImmuneTime(-1, (int) (player.immuneTime * 0.75f)); //Check ImmunityCooldownID for the first int and the second int is the time to add (0.8 is 75% of 1 so 25% increased immunity)
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<KanumBar>(), 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
