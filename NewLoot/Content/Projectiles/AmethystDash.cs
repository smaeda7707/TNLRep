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
    internal class AmethystDash : ModProjectile
    {
        private int immune;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;

            Projectile.penetrate = 2;
            Projectile.timeLeft = 45;
            Projectile.extraUpdates = 2;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (Projectile.timeLeft == 45)
            {
                Projectile.damage = (int) (Projectile.damage * 1.75f);
            }
            if (Projectile.timeLeft == 35)
            {
                Projectile.damage = (int)(Projectile.damage / 1.75f);
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.ToRadians(45);
            Projectile.velocity *= 0.99f;

            if (immune > 0)
            {
                owner.immuneTime = 5;
                owner.immune = true;
                immune--;
            }

            Lighting.AddLight(Projectile.Center, new Color(165, 0, 236).ToVector3() * 0.5f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            if (Projectile.timeLeft >= 15)
            {
                immune = 60;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst, Projectile.velocity.X * Main.rand.NextFloat(0.6f, 0.9f), Projectile.velocity.Y * Main.rand.NextFloat(0.6f, 0.7f), 100, default, 1.2f);
                dust.noGravity = true;
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

    }
}
