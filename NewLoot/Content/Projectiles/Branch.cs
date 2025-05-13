using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;

namespace NewLoot.Content.Projectiles
{
    internal class Branch : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 25;// the cooldown between hits (For local NPC i frames)
            Projectile.scale = 1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == 58)
            {
                Projectile.Resize(14, 34);
                Projectile.frame++;
            }
            if (Projectile.timeLeft == 56)
            {
                Projectile.Resize(18, 48);
                Projectile.frame++;
            }
            if (Projectile.timeLeft == 54)
            {
                Projectile.Resize(26, 60);
                Projectile.frame++;
            }


                float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                // Original Excalibur color: Color.Gold, Color.White
                Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);
                Dust coloredDust = Dust.NewDustPerfect(Projectile.Bottom + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 4f * Projectile.scale), DustID.Clentaminator_Green, dustVelocity * 1f, 100, dustColor, 0.4f);
                coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                coloredDust.scale = 1f;
                coloredDust.noGravity = true;

            
        }
    }
}
