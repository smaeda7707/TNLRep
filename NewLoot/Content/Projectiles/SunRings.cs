﻿using Microsoft.Xna.Framework.Graphics;
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
    internal class SunRings : ModProjectile
    {
        private int hits;
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 32;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;

            Projectile.DamageType = DamageClass.Melee;

            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;

            Projectile.timeLeft = 25;

            Projectile.extraUpdates = 2;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }
        public override void AI()
        {
            Color color = new Color(227, 120, 50);



            Lighting.AddLight(Projectile.Center, color.ToVector3() * 0.5f);



            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

            // Original Excalibur color: Color.Gold, Color.White
            Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);
            Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 6f * Projectile.scale), DustID.Torch, dustVelocity * 1f, 100, dustColor, 0.4f);
            coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
            coloredDust.scale = 1f;
            coloredDust.noGravity = true;

            

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightYellow;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 90);

            if (hits == 3)
            {
                Projectile.friendly = false;
            }
            hits++;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, Projectile.velocity.X * Main.rand.NextFloat(0.7f, 0.8f), Projectile.velocity.Y * Main.rand.NextFloat(0.7f, 0.8f), 100, default, 1.5f);
                dust.noGravity = true;
            }
        }

    }

}
