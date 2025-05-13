using Microsoft.Xna.Framework;
using Mono.Cecil;
using NewLoot.Common.Global;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Weapons
{
    internal class PoisonKnives : ModItem
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
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 34; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 34; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.noUseGraphic = true;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 11; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.shootSpeed = 12f;

            loadPerUse = 3;
            reloadUseSpeed = 28;
            maxLoad = 12;

            Item.shoot = ModContent.ProjectileType<PoisonKnife>();
            
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 12)
                .AddIngredient(ItemID.JungleSpores, 6)
                .AddTile(TileID.Anvils)
                .Register();
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

            }
            else
            {
                if (gunLoad != maxLoad)
                {
                    gunLoad = GlobalItemMethods.CreateRotatedProjectilesWithGunLoad(player, source, position, velocity, ModContent.ProjectileType<PoisonKnife>(), damage, 12, gunLoad, 3, knockback, true);
                }
                else
                {
                    GlobalItemMethods.CreateRotatedProjectiles(player, source, position, velocity, ModContent.ProjectileType<PoisonKnife>(), damage, 20, 18, knockback, false);
                    gunLoad = 0;
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
                    Item.GetGlobalItem<GlobalFields>().energyCost = 20;
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
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;

                if (gunLoad <= 0)
                {
                    return false;
                }
                else
                {
                    Item.useTime = 34;
                    Item.useAnimation = 34;

                    // The sound that this item plays when used.
                    if (gunLoad == maxLoad) {
                        Item.UseSound = SoundID.DD2_MonkStaffSwing;
                    }
                    else
                    {
                        Item.UseSound = SoundID.Item1;
                    }

                    return true;
                }
            }

        }
    }
}
