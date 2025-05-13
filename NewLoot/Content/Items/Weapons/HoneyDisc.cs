using Newloot.Content.Projectiles;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using Terraria.GameContent.ObjectInteractions;
namespace Newloot.Content.Items.Weapons
{
    internal class HoneyDisc : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 32; // The width of the item's hitbox.
            Item.height = 32; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Swing; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 18; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 18; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 29; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 6.2f; // The amount of knockback the item inflicts.
            Item.rare = ItemRarityID.Green; // The item's rarity. This changes the color of the item's name.a
            Item.value = Item.buyPrice(gold: 0, silver: 60); // The amount of money that the item is can be bought for.

            Item.shootSpeed = 11.7f; // The velocity of the shot projectile.			
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                int count = 0;
                Item.GetGlobalItem<GlobalFields>().energyCost = 17;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                Item.shoot = (ModContent.ProjectileType<HoneyDiscProjSecondary>());
                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                    {
                        count++;
                    }
                }
                if (count >= 2)
                {
                    return false;
                }
                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                int count = 0;
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 18;
                Item.useTime = 18;
                Item.UseSound = SoundID.Item1;
                Item.shoot = (ModContent.ProjectileType<HoneyDiscProj>());

                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                    {
                        count++;
                    }
                }
                if (count >= 2)
                {
                    return false;
                }
                return true;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrispyHoneyBlock, 18);
            recipe.AddIngredient(ItemID.BeeWax, 10);
            recipe.AddIngredient(ItemID.BottledHoney, 8);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
