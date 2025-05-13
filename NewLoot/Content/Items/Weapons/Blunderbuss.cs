using Microsoft.Xna.Framework;
using Mono.Cecil;
using NewLoot.Common.Global;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Weapons
{
    internal class Blunderbuss : ModItem
    {
        public static int gunLoad = 0;
        private int reloadUseSpeed;
        private int loadPerUse = 1;
        public static int maxLoad;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true; // Allows right click to be autoswing
        }
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 56; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 56; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 6; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 12f;
            Item.value = Item.sellPrice(0, 0, 90);

            loadPerUse = 6;
            reloadUseSpeed = 40;
            maxLoad = 12;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1.3f, -0.1f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //TODO: Move this to a more specifically named example. Say, a paint gun?
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {

                // Gun Properties
                Item.shoot = (ModContent.ProjectileType<Null>()); // For some reason, all the guns in the vanilla source have this.
                Item.useAmmo = AmmoID.None; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.

            }
            else
            {
                if (gunLoad == maxLoad && player.GetModPlayer<Energy>().energyCurrent > 20)
                {
                    if (Main.rand.NextBool(8))
                    {
                        const int NumProjectiles = 12; // The humber of projectiles that this gun will shoot.

                        for (int i = 0; i < NumProjectiles; i++)
                        {
                            // Rotate the velocity randomly by 30 degrees at max.
                            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(26));

                            // Decrease velocity randomly for nicer visuals.
                            newVelocity *= 1.2f - Main.rand.NextFloat(0.3f);

                            // Create a projectile.
                            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                        }
                        player.GetModPlayer<Energy>().energyCurrent -= 20;
                        gunLoad -= 12;
                    }
                    else
                    {
                        if (Main.rand.NextBool(5))
                        {
                            const int NumProjectiles = 8;
                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                // Rotate the velocity randomly by 30 degrees at max.
                                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(24));

                                // Decrease velocity randomly for nicer visuals.
                                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                                // Create a projectile
                                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                            }


                        }
                        else
                        {
                            const int NumProjectiles = 6; // The humber of projectiles that this gun will shoot.

                            for (int i = 0; i < NumProjectiles; i++)
                            {
                                // Rotate the velocity randomly by 30 degrees at max.
                                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(18));

                                // Decrease velocity randomly for nicer visuals.
                                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                                // Create a projectile.
                                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                            }
                        }
                        gunLoad -= 6;
                    }
                }
                else
                {
                    if (Main.rand.NextBool(5))
                    {
                        const int NumProjectiles = 8;
                        for (int i = 0; i < NumProjectiles; i++)
                        {
                            // Rotate the velocity randomly by 30 degrees at max.
                            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(24));

                            // Decrease velocity randomly for nicer visuals.
                            newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                            // Create a projectile
                            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                        }


                    }
                    else
                    {
                        const int NumProjectiles = 6; // The humber of projectiles that this gun will shoot.

                        for (int i = 0; i < NumProjectiles; i++)
                        {
                            // Rotate the velocity randomly by 30 degrees at max.
                            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(18));

                            // Decrease velocity randomly for nicer visuals.
                            newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                            // Create a projectile.
                            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

                        }
                    }
                    gunLoad -= 6;
                }
            }
               
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                gunLoad = ReloadGlobal.Reload(player, Item, gunLoad, maxLoad, reloadUseSpeed, loadPerUse);

                if (gunLoad >= maxLoad)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        
            }
            else
            {
                if (gunLoad <= 0)
                {
                    return false;
                }
                else
                {
                    Item.useTime = 56;
                    Item.useAnimation = 56;

                    // Gun Properties
                    Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
                    Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.


                    // The sound that this item plays when used.
                    Item.UseSound = SoundID.Item36;

                    return true;
                }
            }

        }
    }
}
