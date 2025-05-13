﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLoot.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace NewLoot.Content.Items.Weapons
{
    internal class Typhoon : ModItem
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
            Item.knockBack = 6.75f; // The knockback of your flail, this is dynamically adjusted in the projectile code.
            Item.width = 30; // Hitbox width of the item.
            Item.height = 10; // Hitbox height of the item.
            Item.damage = 58; // The damage of your flail, this is dynamically adjusted in the projectile code.
            Item.crit = 0; // Critical damage chance %
            Item.scale = 1.1f;
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.shoot = ModContent.ProjectileType<TyphoonProjectile>(); // The flail projectile
            Item.shootSpeed = 12f; // The speed of the projectile measured in pixels per frame.
            Item.UseSound = SoundID.Item1; // The sound that this item makes when used
            Item.rare = ItemRarityID.Pink; // The color of the name of your item
            Item.value = Item.sellPrice(gold: 5, silver: 20); // Sells for 2 gold 50 silver
            Item.DamageType = DamageClass.MeleeNoSpeed; // Deals melee damage
            Item.channel = true;
            Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
        }


        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    }
}
