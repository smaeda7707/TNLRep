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
    internal class SunRod : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            // FOR STAFF LIKE BEHAVIOR WHEN SHOOTING
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 30; // Hitbox width of the item.
            Item.height = 30; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 46; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 46; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item25;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 13; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shootSpeed = 4.5f; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 7;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 58;
                Item.UseSound = SoundID.Item60;
                Item.mana = 0;
                Item.shoot = ModContent.ProjectileType<SunOrbLarge>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item20;
                Item.mana = 7;
                Item.shoot = ModContent.ProjectileType<SunOrb>();

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
                Projectile.NewProjectile(source, position, velocity * 0.12f, type, 0, 0);
                return false;
            }
            else
            {
                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
        }
    }
}
