using System;
using NewLoot.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using NewLoot.Common.Global;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using ReLogic.Content;

namespace NewLoot.Content.Projectiles.Minions
{
    // This file contains all the code necessary for a minion
    // - ModItem - the weapon which you use to summon the minion with
    // - ModBuff - the icon you can click on to despawn the minion
    // - ModProjectile - the minion itself

    // It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overwiew.
    // To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
    // This is NOT an in-depth guide to advanced minion AI

    public class SnatcherSentryBuff : ModBuff
    {
       
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SnatcherSentry>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class SnatcherSentryItem : ModItem
    {
        public static bool secondary;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 

        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 17;
                Item.shoot = ModContent.ProjectileType<Null>(); // This item creates the minion projectile

                var exampleResourcePlayer = player.GetModPlayer<Energy>();

                return exampleResourcePlayer.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<SnatcherSentry>(); // This item creates the minion projectile
                return base.CanUseItem(player);
            }
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                secondary = true;
                var exampleResourcePlayer = player.GetModPlayer<Energy>();

                exampleResourcePlayer.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Vine, 7)
                .AddIngredient(ItemID.JungleSpores, 6)
                .AddIngredient(ModContent.ItemType<SnatcherJaw>())
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 0.8f;
            Item.mana = 10; // mana cost
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(silver: 36);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item44; // What sound should play when using the item

            // These below are needed for a minion weapon
            Item.noMelee = true; // this item doesn't do any melee damage
            Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
            Item.buffType = ModContent.BuffType<SnatcherSentryBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<SnatcherAnchor>(), damage, 0,  player.whoAmI, player.whoAmI, player.whoAmI, player.whoAmI);
            } 

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, player.whoAmI, player.whoAmI, player.whoAmI);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }
    }

    // This minion shows a few mandatory things that make it behave properly.
    // Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
    // If the player targets a certain NPC with right-click, it will fly through tiles to it
    // If it isn't attacking, it will float near the player with minimal movement
    public class SnatcherSentry : ModProjectile
    {
        private int timer = 0;
        private int timer2 = 0;
        private bool check;
        private Item sourceItem;
        public Color color1;
        private const string ChainTexturePath = "NewLoot/Content/Projectiles/Minions/SnatcherChain"; // The folder path to the flail chain sprite
        private const string ChainTextureExtraPath = "NewLoot/Content/Projectiles/Minions/SnatcherChain2";  // This texture and related code is optional and used for a unique effect

        public override void OnSpawn(IEntitySource source)
        {
            Main.player[Projectile.owner].UpdateMaxTurrets(); // Used for sentries

            if (source is EntitySource_ItemUse entitySourceItemUse)
                sourceItem = entitySourceItemUse.Item;
        }
        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 3;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = false;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 26;
            Projectile.tileCollide = false; // Makes the minion go through tiles freely

            // These below are needed for a minion weapon
            Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.sentry = true; // Declares this as a minion (has many effects)
            Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
            Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 20;// the cooldown between hits (For local NPC i frames)
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            color1 = lightColor;
            return base.GetAlpha(lightColor);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (check)
            {
                modifiers.FinalDamage *= 3;
            }
        }

        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile anchor = Main.projectile[Projectile.whoAmI - 1];

            if (SnatcherSentryItem.secondary)
            {
                if (timer == 10)
                {
                    SnatcherSentryItem.secondary = false;
                    timer = 0;
                }
                else
                {
                    timer++;
                }

                check = true;
            }
            if (check)
            {
                if (timer2 == 38)
                {
                    check = false;
                    timer2 = 0;
                }
                else
                {
                    timer2 ++;
                }
            }


            if (!CheckActive(owner))
            {
                return;
            }

            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition, anchor);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition, anchor);
            Visuals(anchor);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<SnatcherSentryBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<SnatcherSentryBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition, Projectile anchor)
        {
            Vector2 idlePosition = anchor.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -anchor.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;

            // Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 100f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 200f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            Projectile.friendly = foundTarget;
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition, Projectile anchor)
        {
            // Default movement parameters (here for attacking)
            float speed = 4f;
            float inertia;
            //float anchorDistance = Vector2.Distance(anchor.Center, Projectile.Center);

            if (SnatcherSentryItem.secondary)
            {
                inertia = 2f;

                // The immediate range around the target (so it doesn't latch onto it when close)
                Vector2 direction = Main.MouseWorld - Projectile.Center;
                direction.Normalize();
                direction *= (speed * 1.5f);

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
            }
            else
            {
                inertia = 20f;
                if (foundTarget && distanceToIdlePosition < 210)
                {
                    // Minion has a target: attack (here, fly towards the enemy)
                    if (distanceFromTarget > 20f)
                    {
                        // The immediate range around the target (so it doesn't latch onto it when close)
                        Vector2 direction = targetCenter - Projectile.Center;
                        direction.Normalize();
                        direction *= speed;

                        Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                    }
                }
                else
                {
                    // Minion doesn't have a target: return to player and idle
                    if (distanceToIdlePosition > 600f)
                    {
                        // Speed up the minion if it's away from the player
                        speed = 6.15f;
                        inertia = 80f;
                    }
                    else
                    {
                        // Slow down the minion if closer to the player
                        speed = 2.8f;
                        inertia = 86f;
                    }

                    if (distanceToIdlePosition > 20f)
                    {
                        // The immediate range around the player (when it passively floats about)

                        // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                        vectorToIdlePosition.Normalize();
                        vectorToIdlePosition *= speed;
                        Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                    }
                    else if (Projectile.velocity == Vector2.Zero)
                    {
                        // If there is a case where it's not moving at all, give it a little "poke"
                        Projectile.velocity.X = -0.08f;
                        Projectile.velocity.Y = -0.03f;
                    }
                }

                    
            }
        }

        private void Visuals(Projectile anchor)
        {

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 7;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4  + 1.5708f;
            }


        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile anchor = Main.projectile[Projectile.whoAmI - 1];
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);
            Asset<Texture2D> chainTextureExtra = ModContent.Request<Texture2D>(ChainTextureExtraPath); // This texture and related code is optional and used for a unique effect

            Rectangle? chainSourceRectangle = null;
            // Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
            float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap. 

            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = anchor.Center.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (chainLengthRemainingToDraw > 0f)
            {
                // This code gets the lighting at the current tile coordinates
                Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

                // Flaming Mace and Drippler Crippler use code here to draw custom sprite frames with custom lighting.
                // Cycling through frames: sourceRectangle = asset.Frame(1, 6, 0, chainCount % 6);
                // This example shows how Flaming Mace works. It checks chainCount and changes chainTexture and draw color at different values

                var chainTextureToDraw = chainTexture;


                // Here, we draw the chain texture at the coordinates
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

                // chainDrawPosition is advanced along the vector back to the player by the chainSegmentLength
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }

            return base.PreDraw(ref lightColor);
        }

    }

    internal class SnatcherAnchor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode

        }

        public override void SetDefaults()
        {
            // While the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            Projectile.friendly = true;

            // 5 second fuse.
            Projectile.timeLeft = 2;
            Projectile.width = 20;
            Projectile.height = 16;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= -0.05f;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {

            Projectile.velocity.Y = Projectile.velocity.Y + 0.3f; // Gravity
            if (Main.projectile[Projectile.whoAmI+1].active)
            {
                Projectile.timeLeft = 2;
            }
            else
            {
                Projectile.Kill();
            }


            // rock can deal damage when falling
            if (Projectile.velocity.Y < 0.8f)
            {
                Projectile.friendly = false;
            }
            else
            {
                Projectile.friendly = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(1f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }


    }
}

