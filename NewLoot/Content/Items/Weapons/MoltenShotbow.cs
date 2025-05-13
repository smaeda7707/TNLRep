using Microsoft.Xna.Framework;
using NewLoot.Common.Global;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using NewLoot.Content.Projectiles;
using System.Data;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewLoot.Content.Items.Weapons
{
    internal class MoltenShotbow : ModItem
    {
        public static int repeaterMaxShots;
        public static int repeaterShots = 0;
        
        private int overheatTime;
        private int heatRegenRate;
        private int shotIncrease;
        private int count = 0;
        private float newVelocity;

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
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            // Use Properties
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item5;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 18; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 7.3f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.

            overheatTime = 300;
            repeaterMaxShots = 7;
            heatRegenRate = 30; // Measured in ticks so 60 = 1 second
            shotIncrease = 1;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 14)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ModContent.ItemType<MoltenString>())
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, -2f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 10;
                Item.useTime = 10;
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.useAnimation = 24;
                Item.useTime = 24;
                repeaterShots = RepeaterGlobal.RepeaterUse(player, repeaterShots, repeaterMaxShots, overheatTime,shotIncrease, false);
            }
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.GetModPlayer<OverheatPlayer>().overheated == true)
                {
                    Item.UseSound = SoundID.Item88;
                    Item.GetGlobalItem<GlobalFields>().energyCost = 2;

                    var Resource = player.GetModPlayer<Energy>();

                    return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Item.UseSound = SoundID.Item5;
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                if (player.GetModPlayer<OverheatPlayer>().overheated == true)
                {
                    return false;
                }
                else
                {
                    return base.CanUseItem(player);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            count = RepeaterGlobal.CountUpdate(player, heatRegenRate, count);
            repeaterShots = RepeaterGlobal.RepeaterUpdate(player, repeaterShots, repeaterMaxShots, heatRegenRate, count);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newPosition = new Vector2(position.X, position.Y - 10);

            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    // Rotate the velocity randomly by 30 degrees at max.
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(35));

                    // Decrease velocity randomly for nicer visuals.
                    newVelocity *= Main.rand.NextFloat(0.35f, 0.45f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, type = ModContent.ProjectileType<MoltenSlag>(), (int) (damage * 0.75f), knockback * 0.3f, player.whoAmI);
                }

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectileDirect(source, newPosition, velocity, type, damage, knockback, player.whoAmI);
                    }
                }
                // Create a projectile.
                
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }



    }
}
