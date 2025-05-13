using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using NewLoot.Common.Players;
using Newloot.Content.Projectiles;
using NewLoot.Common.Global;

namespace NewLoot.Content.Items.Weapons
{
    public class CrescentSickle : ModItem
    {
        
        public int attackType = 0; // keeps track of which attack it is
        public int comboExpireTimer = 0; // we want the attack pattern to reset if the weapon is not used for certain period of time

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 62;
            Item.height = 58;
            Item.value = Item.sellPrice(gold: 2, silver: 25);
            Item.rare = ItemRarityID.Orange;

            // Use Properties
            // Note that useTime and useAnimation for this item don't actually affect the behavior because the held projectile handles that. 
            // Each attack takes a different amount of time to execute
            // Conforming to the item useTime and useAnimation makes it much harder to design
            // It does, however, affect the item tooltip, so don't leave it out.
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Shoot;

            // Weapon Properties
            Item.knockBack = 7.95f;  // The knockback of your sword, this is dynamically adjusted in the projectile code.
            Item.autoReuse = true; // This determines whether the weapon has autoswing
            Item.damage = 42; // The damage of your sword, this is dynamically adjusted in the projectile code.
            Item.DamageType = DamageClass.Melee; // Deals melee damage
            Item.noMelee = true;  // This makes sure the item does not deal damage from the swinging animation
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand

            // Projectile Properties
            Item.shoot = ModContent.ProjectileType<CrescentSickleProjectile>(); // The sword as a projectile
            Item.shootSpeed = 1.8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
            if (player.altFunctionUse != 2)
            {
                attackType = (attackType + 1) % 2; // Increment attackType to make sure next swing is different
                comboExpireTimer = 0; // Every time the weapon is used, we reset this so the combo does not expire
                
            }
            return false; // return false to prevent original projectile from being shot
        }

        public override void UpdateInventory(Player player)
        {
            if (comboExpireTimer++ >= 120) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
                attackType = 0;
        }

        public override bool MeleePrefix()
        {
            return true; // return true to allow weapon to have melee prefixes (e.g. Legendary)
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 32;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                Item.shoot = ModContent.ProjectileType<CrescentSickleProjSecondary>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 36;
                Item.useTime = 36;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<CrescentSickleProjectile>();

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
    }
}
