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
    public class SunKnife : ModItem
    {
        private int attacks;
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 3.2f;
            Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.width = 38;
            Item.height = 38;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 10);

            Item.shoot = ModContent.ProjectileType<SunKnifeProjectile>(); // The projectile is what makes a shortsword work
            Item.shootSpeed = 10.2f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI, 0, 100);
            }
            else
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7));
                if (attacks % 5 == 0)
                {
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI, 0, 200);
                }
                else
                {
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
                attacks++;
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 23;
                Item.useTime = 22;
                Item.useAnimation = 22;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost && player.ownedProjectileCounts[Item.shoot] < 1;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 12;
                Item.useTime = 12;

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
