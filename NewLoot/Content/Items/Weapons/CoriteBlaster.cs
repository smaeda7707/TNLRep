using Microsoft.Xna.Framework;
using Mono.Cecil;
using NewLoot.Common.Global;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Weapons
{
    internal class CoriteBlaster : ModItem
    {
        public static int gunLoad = 0;
        private int reloadUseSpeed;
        private int loadPerUse;
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
            Item.scale = 0.65f;
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 19; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 19; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 12; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.9f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 14f;

            loadPerUse = 1;
            reloadUseSpeed = 14;
            maxLoad = 8;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CoriteBar>(), 10)
                .AddIngredient(ItemID.Leather, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1.5f, 0.3f);
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
                // Every projectile shot from this gun has a 1/3 chance of being an ExampleInstancedProjectile
                if (Main.rand.NextBool(4))
                {
                    Item.shootSpeed = 16f;
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                else
                {
                    Item.shootSpeed = 14f;
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                gunLoad -= 1;
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
                if (gunLoad == 0)
                {
                    return false;
                }
                else
                {
                    Item.useTime = 19;
                    Item.useAnimation = 19;

                    // Gun Properties
                    Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
                    Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.


                    // The sound that this item plays when used.
                    Item.UseSound = SoundID.Item41;

                    return true;
                }
            }

        }
    }
}
