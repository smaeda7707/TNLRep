using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using NewLoot.Content.Items.Accessories;

namespace NewLoot.Content.Items.Weapons
{
    internal class NatureBow : ModItem
    {
        
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 16; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 26; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 26; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item5;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 7; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 0.7f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 6f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 25;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item69;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 26;
                Item.useTime = 26;
                Item.UseSound = SoundID.Item5;

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
            Vector2 newPosition = new Vector2(position.X + 36, position.Y - 5);
            Vector2 newPosition2 = new Vector2(position.X - 36, position.Y - 5);
            if (player.altFunctionUse == 2)
            {
                if (player.direction == 1)
                {
                    Projectile.NewProjectileDirect(source, newPosition, velocity * 0, type = ModContent.ProjectileType<Branch>(), (int)(damage * 1.75f), knockback * 0, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectileDirect(source, newPosition2, velocity * 0, type = ModContent.ProjectileType<Branch>(), (int)(damage * 1.75f), knockback * 0, player.whoAmI);
                }
            }
            else
            {
                    // Create a projectile.
                    Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }
    }
}
