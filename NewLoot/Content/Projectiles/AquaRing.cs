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
    internal class AquaRing : ModProjectile
    {
        public static int realTimeLeft = 125;
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;

            Projectile.penetrate = -1;
            Projectile.timeLeft = realTimeLeft;
            Projectile.scale = 1.2f;

            DrawOriginOffsetX = 5;
            DrawOriginOffsetY = 5;

        }
        public override void AI()
        {
            Projectile.alpha = 130;

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;
            Projectile.velocity *= 0.98f;

            for (int i = 0; i < 2; i++)
            {
                float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                // Original Excalibur color: Color.Gold, Color.White
                Color dustColor = Color.Lerp(Color.Blue, Color.RoyalBlue, Main.rand.NextFloat() * 0.3f);
                Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 10f * Projectile.scale), DustID.Water, dustVelocity * 1f, 100, dustColor, 0.4f);
                coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                coloredDust.scale = 1f;
                coloredDust.noGravity = true;

            }

            if (Main.player[Projectile.owner].GetModPlayer<AquaBuffPlayer>().aquaBuffed == true)
            {
                realTimeLeft = 250;
            }
            else
            {
                realTimeLeft = 125;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.damage <= 4)
            {
                Projectile.Kill();
            }
            else
            {
                if (Main.player[Projectile.owner].GetModPlayer<AquaBuffPlayer>().aquaBuffed == true)
                {
                    Projectile.damage = (int)(Projectile.damage * 0.75f);
                }
                else
                {
                    Projectile.damage = (int)(Projectile.damage * 0.5f);
                }
            }

        }

    }
}
