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
    internal class ShotgunBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Ranged;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;

            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 25;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft <= 540)
            {
                return Color.White;
            }
            else
            {
                return base.GetAlpha(lightColor);
            }
        }
        public override void AI()
        {
            float rotateSpeed = 0.2f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            if (Projectile.timeLeft <= 80)
            {
                Projectile.velocity *= 0.9f;
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
        public override void OnKill(int timeLeft)
        {
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();
            Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);

            Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 5f * Projectile.scale), DustID.WaterCandle, dustVelocity * 1f, 100, dustColor, 0.4f);
            coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
            coloredDust.scale = 2f;
            coloredDust.noGravity = true;
        }

    }


    internal class ShotgunBladeBig : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Ranged;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft <= 540)
            {
                return Color.White;
            }
            else
            {
                return base.GetAlpha(lightColor);
            }
        }
        public override void AI()
        {
            float rotateSpeed = 0.35f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            if (Projectile.timeLeft <= 150)
            {
                Projectile.velocity *= 0.9f;
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
        public override void OnKill(int timeLeft)
        {
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();
            Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);

            Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 5f * Projectile.scale), DustID.WaterCandle, dustVelocity * 1f, 100, dustColor, 0.4f);
            coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
            coloredDust.scale = 2f;
            coloredDust.noGravity = true;
        }

    }
}
