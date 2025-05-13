using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class AdamantiteChest : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 13;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AdamantiteChestPlayer>().equipped = true;
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 22);
            recipe.AddIngredient(ItemID.PlatinumBar, 10);
            recipe.AddIngredient(ItemID.CrystalShard, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class AdamantiteChestPlayer : ModPlayer
    {
        private int timer;
        // The fields related to the dash accessory
        public bool equipped;
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (equipped)
            {
                velocity *= 1.08f;
            }
        }
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            MakeSureEquipWork();
        }
        public void MakeSureEquipWork()
        {
            if (timer == 0)
            {
                timer++;
            }
            else
            {
                timer = 0;
                equipped = false;
            }
        }
    }
}
