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
    internal class WindChargeExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.5f;

            DrawOffsetX = 37;
            DrawOriginOffsetY = 37;
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 255;
            Projectile.Center = Main.player[Projectile.owner].Center;
            if (Projectile.timeLeft == 10)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
            }
            if (Projectile.timeLeft <= 10)
            {
                Projectile.friendly = true;
                for (int i = 0; i < 10; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.WhiteSmoke, Color.LightSkyBlue, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 15f * Projectile.scale), DustID.Cloud, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.5f;
                    coloredDust.noGravity = true;

                }
                for (int i = 0; i < 6; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.WhiteSmoke, Color.LightSkyBlue, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 15f * Projectile.scale), DustID.AncientLight, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.5f;
                    coloredDust.noGravity = true;

                }

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                        Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                        Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                        // Original Excalibur color: Color.Gold, Color.White
                        Color dustColor = Color.Lerp(Color.WhiteSmoke, Color.LightSkyBlue, Main.rand.NextFloat() * 0.3f);
                        Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (80f * Projectile.scale + 15f * Projectile.scale), DustID.AncientLight, dustVelocity * -24f, 100, dustColor, 0.4f);
                        coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                        coloredDust.scale = 0.8f;
                        coloredDust.noGravity = true;
                    }

                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go away from player
            modifiers.HitDirectionOverride = target.position.X > Main.player[Projectile.owner].MountedCenter.X ? 1 : -1;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int) (Projectile.damage * 0.75f);
        }
    }
}
