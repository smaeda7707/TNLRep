using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;
using System;
using Humanizer;

namespace NewLoot.Content.Items.Weapons
{
    internal class ModernBow : ModItem
    {
        
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 26; // Hitbox width of the item.
            Item.height = 46; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = Item.sellPrice(0, 2, 25);

            // Use Properties
            Item.useTime = 36; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 36; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item5;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 47; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.crit = 3;

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 9.4f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<KanumBar>(), 7)
                .AddIngredient(ItemID.FlinxFur , 5)
                .AddIngredient(ItemID.Silk , 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1.5f, 0f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 68;
                Item.UseSound = SoundID.Item63;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                if (player.HasBuff(ModContent.BuffType<ModernBowBuff>()))
                {
                    Item.useTime = 24; // The item's use time in ticks (60 ticks == 1 second.)
                    Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                }
                else
                {
                    Item.useTime = 36; // The item's use time in ticks (60 ticks == 1 second.)
                    Item.useAnimation = 36; // The length of the item's use animation in ticks (60 ticks == 1 second.)
                }
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item5;

                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<ModernBowBuff>(), 1080);

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
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Null>(), damage * 0, knockback, player.whoAmI);
            }
            else
            {
                    // Create a projectile.
                    Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            }


            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.HasBuff(ModContent.BuffType<ModernBowBuff>()))
            {
                float maxDetectRadius = 150f; // The maximum radius at which a projectile can detect a target
                float projSpeed = 10f; // The speed at which the projectile moves towards the target

                // Trying to find NPC closest to the projectile
                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC != null)
                {
                    // If found, change the velocity of the projectile and turn it in the direction of the target
                    // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
                    velocity = (closestNPC.Center - player.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                }
                    velocity *= 2f;
            }
            
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Main.MouseWorld);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

    }
}
