using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Weapons
{
    internal class AngelStaff : ModItem
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
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 46; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 46; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item72;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 13; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 4.6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 11.5f; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 12;
        }
      //  public override Vector2? HoldoutOffset()
       // {
           // return new Vector2(1.3f, -5.5f);
      //  }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 60;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.mana = 0;
                Item.UseSound = SoundID.Item35;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 40;
                Item.useTime = 40;
                Item.mana = 12;
                Item.UseSound = SoundID.Item72;

                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<Halo>(), 900);
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

            float targetAngle = (Main.MouseWorld - player.MountedCenter).ToRotation();

            if (player.altFunctionUse != 2)
            {
                if (player.direction == 1)
                {
                    int rotateNum = -20;
                    Vector2 newPosition = new Vector2(position.X, position.Y + 120);
                    const int NumProjectiles = 3;
                    for (int i = 0; i < NumProjectiles; i++)
                    {
                        // Rotate the velocity randomly by 30 degrees at max.
                        Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNum));

                        // Create a projectile.
                        Projectile.NewProjectileDirect(source, newPosition, newVelocity, type = ModContent.ProjectileType<HolyBeam>(), damage, knockback, player.whoAmI);

                        if (i == 0)
                        {
                            newPosition = position;
                            rotateNum = 0;
                        }
                        else if (i == 1)
                        {
                            newPosition = new Vector2(position.X, position.Y - 120);
                            rotateNum = 20;
                        }
                    }
                }
                else
                {
                    int rotateNum = 20;
                    Vector2 newPosition = new Vector2(position.X, position.Y + 120);
                    const int NumProjectiles = 3;
                    for (int i = 0; i < NumProjectiles; i++)
                    {
                        // Rotate the velocity randomly by 30 degrees at max.
                        Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNum));

                        // Create a projectile.
                        Projectile.NewProjectileDirect(source, newPosition, newVelocity, type = ModContent.ProjectileType<HolyBeam>(), damage, knockback, player.whoAmI);

                        if (i == 0)
                        {
                            newPosition = position;
                            rotateNum = 0;
                        }
                        else if (i == 1)
                        {
                            newPosition = new Vector2(position.X, position.Y - 120);
                            rotateNum = -20;
                        }
                    }
                }
            }
        


            return false; // Return false because we don't want tModLoader to shoot projectile
        }
    }
}
