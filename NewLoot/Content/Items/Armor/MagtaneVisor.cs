using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Items.Accessories;
using System.Drawing.Drawing2D;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MagtaneVisor : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 3;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<MagtaneVisorPlayer>().MagtaneVisorEquipped = true;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagtaneBar>(), 10);
            recipe.AddIngredient(ItemID.YellowStainedGlass, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased ranged critical strike chance by 8%\nDecreased ranged damage by 10%";
            player.GetCritChance(DamageClass.Ranged) += 8f;
            player.GetDamage(DamageClass.Ranged) -= 0.10f; ;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<MagtaneBoots>() && body.type == ModContent.ItemType<MagtaneChestplate>())
            {
                return true;
            }
            return false;
        }

    }
    public class MagtaneVisorPlayer : ModPlayer
    {
        public bool MagtaneVisorEquipped;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            MagtaneVisorEquipped = false;
        }
    }
}
