using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NewLoot.Content.Projectiles;
using Newloot.Content.Projectiles;
using NewLoot.Common.Global;
using NewLoot.Common.Players;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Items.Weapons
{
    public class IceRapier : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.knockBack = 4f;
            Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.width = 54;
            Item.height = 54;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 10);

            Item.shoot = ModContent.ProjectileType<IceRapierProjectile>(); // The projectile is what makes a shortsword work
            Item.shootSpeed = 10.2f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectileDirect(source, position, Vector2.Zero, ModContent.ProjectileType<IceRapierSpin>(), (int) (damage * 1.25f), knockback * 2, player.whoAmI);
            }
            else
            {
                // Create a projectile.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 33;
                Item.useTime = 90;
                Item.useAnimation = 90;
                Item.useStyle = ItemUseStyleID.RaiseLamp; // Makes the player do the proper arm motion

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 16;
                Item.useTime = 16;
                Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion

                // Ensures no more than one spear can be thrown out, use this when using autoReuse
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
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
                if (!Main.dedServ && Item.UseSound.HasValue)
                {
                    SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
                }
                return true;
            }


        }
    }
}
