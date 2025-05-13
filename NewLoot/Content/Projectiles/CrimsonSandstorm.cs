using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Projectiles
{
    internal class CrimsonSandstorm : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.ArmorPenetration = 10;
            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 25;// the cooldown between hits (For local NPC i frames)
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.5f;

            DrawOffsetX = 150;
            DrawOriginOffsetY = 150;
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 185;


            if (Main.rand.NextBool(2))
            {
                for (int i = 0; i < 16; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    // Original Excalibur color: Color.Gold, Color.White
                    Color dustColor = Color.Lerp(Color.Maroon, Color.Red, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 300f * Projectile.scale + 20f * Projectile.scale), DustID.Sand, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.5f;
                    coloredDust.noGravity = true;

                }
            }
            if (Main.rand.NextBool(4))
            {
                for (int i = 0; i < 8; i++)
                {
                    float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                    Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                    Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                    Color dustColor2 = Color.Lerp(Color.Maroon, Color.Red, Main.rand.NextFloat() * 0.3f);
                    Dust coloredDust2 = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 300f * Projectile.scale + 20f * Projectile.scale), DustID.Cloud, dustVelocity * 1f, 100, dustColor2, 0.4f);
                    coloredDust2.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust2.scale = 2.5f;
                    coloredDust2.noGravity = true;
                }
            }
            SimpleCustomCollisionLogic(Projectile.getRect(), 1);

        }
        public void SimpleCustomCollisionLogic(Rectangle projRect, int allowed)
        {
            //var type = member.Key;
            //var time = member.Value.Time;

            //allowed = 1 for only players, 2 for only npc's, 0 for both
            if (allowed != 1)
            {
                int index = Array.FindIndex(Main.npc, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    //Main.npc[index].AddBuff(type, time);
                    //dict.Remove(type);
                }
            }
            if (allowed != 2)
            {
                int index = Array.FindIndex(Main.player, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    Main.player[index].AddBuff(ModContent.BuffType<CrimsonSandstormBuff>(), 1);
                    //dict.Remove(type);
                }
            }
        }

    }
}
