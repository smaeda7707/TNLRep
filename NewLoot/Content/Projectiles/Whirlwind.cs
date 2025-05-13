using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using NewLoot.Content.Buffs;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class Whirlwind : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 210;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 7;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            Projectile.alpha = 80;
            float rotateSpeed = 0.3f * Projectile.direction;
            Projectile.rotation += rotateSpeed;
            for (int i = 0; i < 2; i++)
            {
                        float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                        Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                        Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                        // Original Excalibur color: Color.Gold, Color.White
                        Color dustColor = Color.Lerp(Color.WhiteSmoke, Color.White, Main.rand.NextFloat() * 0.3f);
                        Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (80f * Projectile.scale + 15f * Projectile.scale), DustID.RainCloud, dustVelocity * -24f, 100, dustColor, 0.4f);
                        coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                        coloredDust.scale = 0.8f;
                        coloredDust.noGravity = true;
                    

            }
            
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go towords projectile center
            modifiers.HitDirectionOverride = target.position.X >Projectile.Center.X ? -1 : 1;
            modifiers.DisableCrit();

        }
    }
}
