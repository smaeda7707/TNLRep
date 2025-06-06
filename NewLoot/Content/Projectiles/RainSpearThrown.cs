﻿
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using NewLoot.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Newloot.Content.Projectiles
{
    internal class RainSpearThrown : ModProjectile
    {
        public override string Texture => "NewLoot/Content/Projectiles/RainSpearProjectile";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        // These properties wrap the usual ai arrays for cleaner and easier to understand code.
        // Are we sticking to a target?
        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        public float StickTimer
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = 70; // The width of projectile hitbox
            Projectile.height = 70; // The height of projectile hitbox
            Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee; // Makes the projectile deal ranged damage. You can set in to DamageClass.Throwing, but that is not used by any vanilla items
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate.
            Projectile.timeLeft = 500; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. Our custom AI below fades our projectile in. Make sure to delete this if you aren't usinga an aiStyle that fades in.
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.hide = true; // Makes the projectile completely invisible. We need this to draw our projectile behind enemies/tiles in DrawBehind()
            Projectile.extraUpdates = 1;
        }

        private const int GravityDelay = 45;

        public override void AI()
        {
            UpdateAlpha();
            // Run either the Sticky AI or Normal AI
            // Separating into different methods helps keeps your AI clean
            if (IsStickingToTarget)
            {
                StickyAI();
            }
            else
            {
                NormalAI();
            }
            if (Projectile.timeLeft%10 == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(0, 0) * 1f, Main.rand.Next(0, 0) * 1f, ModContent.ProjectileType<Raindrop>(), (int)(Projectile.damage/3), 0, Projectile.owner);

            }

        }

        private void NormalAI()
        {
            GravityDelayTimer++; // doesn't make sense.

            // For a little while, the javelin will travel with the same speed, but after this, the javelin drops velocity very quickly.
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;

                // wind resistance
                Projectile.velocity.X *= 0.98f;
                // gravity
                Projectile.velocity.Y += 0.35f;
            }

            // Offset the rotation by 90 degrees because the sprite is oriented vertiacally.
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

        }

        private const int StickTime = 60 * 15; // 15 seconds
        private void StickyAI()
        {
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            StickTimer += 1f;

            // Every 30 ticks, the javelin will perform a hit effect
            bool hitEffect = StickTimer % 30f == 0f;
            int npcTarget = TargetWhoAmI;
            if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
            { // If the index is past its limits, kill it
                Projectile.Kill();
            }
            else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
            {
                // If the target is active and can take damage
                // Set the projectile's position relative to the target's center
                Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
                if (hitEffect)
                {
                    // Perform a hit effect here, causing the npc to react as if hit.
                    // Note that this does NOT damage the NPC, the damage is done through the debuff.
                    Main.npc[npcTarget].HitEffect(0, 1.0);
                }
            }
            else
            { // Otherwise, kill the projectile
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
            Vector2 usePos = Projectile.position; // Position to use for dusts

            // Offset the rotation by 90 degrees because the sprite is oriented vertiacally.
            Vector2 rotationVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
            usePos += rotationVector * 16f;


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

        private const int MaxStickingJavelin = 6; // This is the max amount of javelins able to be attached to a single NPC
        private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin]; // The point array holding for sticking javelins

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            // For going through platforms and such, javelins use a tad smaller size
            width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
            return true;
        }


        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            // If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
            if (IsStickingToTarget)
            {
                int npcIndex = TargetWhoAmI;
                if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                {
                    if (Main.npc[npcIndex].behindTiles)
                    {
                        behindNPCsAndTiles.Add(index);
                    }
                    else
                    {
                        behindNPCsAndTiles.Add(index);
                    }

                    return;
                }
            }
            // Since we aren't attached, add to this list
            behindNPCsAndTiles.Add(index);
        }

        // Change this number if you want to alter how the alpha changes
        private const int AlphaFadeInSpeed = 25;

        private void UpdateAlpha()
        {
            // Slowly remove alpha as it is present
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= AlphaFadeInSpeed;
            }

            // If alpha gets lower than 0, set it to 0
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

    }
}
