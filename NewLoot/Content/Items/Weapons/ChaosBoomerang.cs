using Newloot.Content.Projectiles;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
namespace Newloot.Content.Items.Weapons
{
    internal class ChaosBoomerang : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 22; // The width of the item's hitbox.
            Item.height = 32; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Swing; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 14; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 14; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 72; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 7.6f; // The amount of knockback the item inflicts.
            Item.crit = 6; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
            Item.rare = ItemRarityID.LightRed; // The item's rarity. This changes the color of the item's name.a
            Item.value = Item.buyPrice(gold: 0, silver: 60); // The amount of money that the item is can be bought for.

            Item.shoot = ModContent.ProjectileType<ChaosBoomerangProj>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
            Item.shootSpeed = 16f; // The velocity of the shot projectile.			
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 36;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.useStyle = ItemUseStyleID.RaiseLamp;
                Item.UseSound = SoundID.Item113;
                Item.shoot = (ModContent.ProjectileType<Null>());

                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active)
                    {
                        var Resource = player.GetModPlayer<Energy>();

                        return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
                    }

                }
                return false;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 20;
                Item.useTime = 20;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = (ModContent.ProjectileType<ChaosBoomerangProj>());

                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                    {
                        return false;
                    }
                }
                return true;
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.position = ChaosBoomerangProj.boomerangPos;
                for (int i = 0; i < 16; i++)
                {
                    Dust dust = Dust.NewDustDirect(player.Center, 10, 10, DustID.TeleportationPotion, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 100, default, 1f);
                    dust.noGravity = true;
                }


                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }

        }
    }
}
