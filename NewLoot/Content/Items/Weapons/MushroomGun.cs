using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;

namespace NewLoot.Content.Items.Weapons
{
    internal class MushroomGun : ModItem
    {
        private bool shooting;
        private int timer;
        private Vector2 shotVelocity;
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 58; // Hitbox width of the item.
            Item.height = 18; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 34; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 34; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item108;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 89; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 3.3f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 61.5f; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 18;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13.5f, -0.5f);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 38;
                Item.mana = 0;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.mana = 18;

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
        public override void UpdateInventory(Player player)
        {
            if (shooting)
            {
                timer++;
            }
            if (timer >= Item.useTime/3)
            {
                shooting = false;
                timer = 0;
                Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, shotVelocity, ProjectileID.Mushroom, (int) (Item.damage * 1.25f), Item.knockBack * 1.3f, player.whoAmI);               
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectileDirect(source, position, velocity/10, ModContent.ProjectileType<MechaMushroom>(), damage, 0, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectileDirect(source, position, velocity, ProjectileID.Mushroom, damage, knockback, player.whoAmI);
                shotVelocity = velocity;
                shooting = true;
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }
    }
}
