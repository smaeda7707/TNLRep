using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;

namespace NewLoot.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)] // Load the spritesheet you create as a shield for the player when it is equipped.
    public class ManaGauntlet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 0, 48);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegen += 1;
            player.GetDamage(DamageClass.Melee) += 0.10f;
            player.GetAttackSpeed(DamageClass.Melee) -= 0.08f;
            player.moveSpeed -= 0.03f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 10)
                .AddIngredient(ItemID.Sapphire, 4)
                .AddIngredient(ItemID.ManaCrystal, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
