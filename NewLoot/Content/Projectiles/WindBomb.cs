using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using NewLoot.Content.Buffs;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace NewLoot.Content.Projectiles
{
    internal class WindBomb : ModProjectile
    {
        private int timer = 0;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 80;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

        }

        public override void AI()
        {
            timer++;
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 255;
            if (Projectile.timeLeft == 10)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
            }
            if (Projectile.timeLeft <= 10)
            {
                
                Projectile.alpha = 255;
                Projectile.Resize(130, 130);
                Projectile.friendly = true;
                Projectile.velocity *= 0;
                for (int i = 0; i < 10; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.White, Color.Gray, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 1 * Projectile.scale), DustID.RainCloud, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.5f;
                    coloredDust.noGravity = true;

                }
                for (int i = 0; i < 2; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.WhiteSmoke, Color.Gray, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 1 * Projectile.scale), DustID.AncientLight, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.5f;
                    coloredDust.noGravity = true;

                }

            }
            else
            {
                Projectile.velocity *= 1.05f;
                Projectile.alpha = 80;
                float rotateSpeed = 0.3f * Projectile.direction;
                Projectile.rotation += rotateSpeed;
                if (timer == 2)
                    {
                        float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                        Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                        Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                        // Original Excalibur color: Color.Gold, Color.White
                        Color dustColor = Color.Lerp(Color.Gray, Color.Gray, Main.rand.NextFloat() * 0.3f);
                        Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (160 * Projectile.scale - 70 * Projectile.scale), DustID.AncientLight, dustVelocity * -24f, 100, dustColor, 0.4f);
                        coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                        coloredDust.scale = 0.8f;
                        coloredDust.noGravity = true;

                        timer = 0;
                    }

                
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
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go away from player
            modifiers.HitDirectionOverride = target.position.X > Main.player[Projectile.owner].MountedCenter.X ? 1 : -1;

        }
    }
}
