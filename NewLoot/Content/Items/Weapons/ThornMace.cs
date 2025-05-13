using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLoot.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Common.Global;
namespace NewLoot.Content.Items.Weapons
{
    internal class ThornMace : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            // This line will make the damage shown in the tooltip twice the actual Item.damage. This multiplier is used to adjust for the dynamic damage capabilities of the projectile.
            // When thrown directly at enemies, the flail projectile will deal double Item.damage, matching the tooltip, but deals normal damage in other modes.
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            // These default values aside from Item.shoot match the Sunfury values, feel free to tweak them.
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.useAnimation = 45; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useTime = 45; // The item's use time in ticks (60 ticks == 1 second.)
            Item.knockBack = 4.6f; // The knockback of your flail, this is dynamically adjusted in the projectile code.
            Item.width = 30; // Hitbox width of the item.
            Item.height = 10; // Hitbox height of the item.
            Item.damage = 9; // The damage of your flail, this is dynamically adjusted in the projectile code.
            Item.crit = 0; // Critical damage chance %
            Item.scale = 1.1f;
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.shoot = ModContent.ProjectileType<ThornMaceProjectile>(); // The flail projectile
            Item.shootSpeed = 10.25f; // The speed of the projectile measured in pixels per frame.
            Item.UseSound = SoundID.Item1; // The sound that this item makes when used
            Item.rare = ItemRarityID.White; // The color of the name of your item
            Item.value = Item.sellPrice(gold: 0, silver: 60); // Sells for 2 gold 50 silver
            Item.DamageType = DamageClass.MeleeNoSpeed; // Deals melee damage
            Item.channel = true;
            Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 15;
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                Item.shoot = ModContent.ProjectileType<ThornMaceProjectileSecondary>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 45;
                Item.useTime = 45;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<ThornMaceProjectile>();

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }


        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.Stinger, 6)
                .AddIngredient(ItemID.Vine, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }


        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    }
}
