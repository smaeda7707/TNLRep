using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Projectiles
{
    internal class GuitarRing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 7;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;

            Projectile.DamageType = DamageClass.Melee;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.scale = 1f;


        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            float rotateSpeed = 0.35f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            if (Projectile.timeLeft <= 15 && Projectile.timeLeft > 10)
            {
                Projectile.width = 14;
                Projectile.height = 14;
            }
            else if (Projectile.timeLeft <= 10 && Projectile.timeLeft > 5)
            {
                Projectile.width = 21;
                Projectile.height = 21;
            }
            else if(Projectile.timeLeft <= 5)
            {
                Projectile.width = 28;
                Projectile.height = 28;
            }

            if (Projectile.timeLeft % 5 == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.Blue, Color.RoyalBlue, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 1f * Projectile.width), DustID.Torch, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.25f;
                    coloredDust.noGravity = true;

                }

            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 480);
        }



    }
}
