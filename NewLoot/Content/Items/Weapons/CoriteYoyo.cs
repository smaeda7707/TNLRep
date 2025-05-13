using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
namespace NewLoot.Content.Items.Weapons
{
    internal class CoriteYoyo : ModItem
    {

        public override void SetStaticDefaults()
        {
            // These are all related to gamepad controls and don't seem to affect anything else
            ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
            ItemID.Sets.GamepadExtraRange[Item.type] = 15; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
        }

        public override void SetDefaults()
        {
            Item.width = 24; // The width of the item's hitbox.
            Item.height = 24; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 16; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 3.75f; // The amount of knockback the item inflicts.
            Item.crit = 0; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
            Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
            Item.rare = ItemRarityID.White; // The item's rarity. This changes the color of the item's name.a
            Item.value = Item.buyPrice(gold: 0, silver: 50); // The amount of money that the item is can be bought for.

            Item.shoot = ModContent.ProjectileType<CoriteYoyoProj>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
            Item.shootSpeed = 19f; // The velocity of the shot projectile.			
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CoriteBar>(), 10)
                .AddIngredient(ItemID.Cobweb, 30)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                 Item.GetGlobalItem<GlobalFields>().energyCost = 20;
                 Item.UseSound = SoundID.Item113;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item1;

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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<CoriteSawblade>(), damage/2, knockback/2);
            }
            return true;
        }
    }
}
