using Microsoft.Xna.Framework;
using Mono.Cecil;
using Newloot.Content.Projectiles;
using NewLoot.Common.Global;
using NewLoot.Common.Players;
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Weapons
{
    internal class SlimeLauncher : ModItem
    {
        public static int gunLoad = 0;
        private int reloadUseSpeed = 40;
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
            Item.width = 52; // Hitbox width of the item.
            Item.height = 20; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 26; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 26; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 23; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 3.4f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            maxLoad = 4;

        }


        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 0.3f);
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
                if (gunLoad == maxLoad)
                {

                    // Create a projectile.
                    Projectile.NewProjectileDirect(source, position, velocity, type = ModContent.ProjectileType<BigSlimeBomb>(), damage * 2, knockback * 1.25f, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item113, player.Center);
                    gunLoad -= 2;
                }
                else
                {
                    Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                    gunLoad -= 1;
                }

            }
               
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            var Resource = player.GetModPlayer<Energy>();

            if (player.altFunctionUse == 2)
            {
                if (gunLoad == maxLoad - loadPerUse)
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 34;
                    if (Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost)
                    {
                        Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                        gunLoad = ReloadGlobal.Reload(player, Item, gunLoad, maxLoad, reloadUseSpeed, loadPerUse);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                    gunLoad = ReloadGlobal.Reload(player, Item, gunLoad, maxLoad, reloadUseSpeed, loadPerUse);
                }


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
                    Item.useTime = 26;
                    Item.useAnimation = 26;

                    // Gun Properties
                    Item.shoot = ModContent.ProjectileType<SlimeBomb>(); ; // For some reason, all the guns in the vanilla source have this.
                    Item.shootSpeed = 9.2f; // The speed of the projectile (measured in pixels per frame.)


                    // The sound that this item plays when used.
                    Item.UseSound = SoundID.Item61;

                    return true;
                }
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

    }
}
