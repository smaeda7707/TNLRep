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
    internal class MagicCards : ModItem
    {
        
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
            Item.noUseGraphic = true;
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item1;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 21; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 3.2f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<MagicCardProj>(); // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 10.3f; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 8;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 15;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item63;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 24;
                Item.useTime = 24;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<MagicCardProj>();

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
            int rotateNum;
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectileDirect(source, position, velocity, type = ModContent.ProjectileType<FurBomb>(), damage * 2, knockback, player.whoAmI);
            }
            else
            {
                rotateNum = -18;
                const int NumProjectiles = 3;
                for (int i = 0; i < NumProjectiles; i++)
                {
                    // Rotate the velocity randomly by 30 degrees at max.
                    Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNum));

                    // Create a projectile.
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                    rotateNum += 18;
                }
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }
    }
}
