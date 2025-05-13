using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class Frostflake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 13;
            Projectile.height = 13;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 0;

            Projectile.penetrate = 2;
            Projectile.timeLeft = 24;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 30;// the cooldown between hits (For local NPC i frames)

        }
        public override void AI()
        {
            float rotateSpeed = 0.5f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            if (Projectile.penetrate == 2)
            {

                    for (int i = 0; i < 6; i++)
                    {
                        float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                        Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                        Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                        // Original Excalibur color: Color.Gold, Color.White
                        Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);
                        Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 6f * Projectile.scale), DustID.FrostStaff, dustVelocity * 1f, 100, dustColor, 0.4f);
                        coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                        coloredDust.scale = 1f;
                        coloredDust.noGravity = true;

                    }
                
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
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

            // If we are the original projectile running on the owner, spawn the 5 child projectiles.
            if (Projectile.owner == Main.myPlayer && Projectile.ai[1] == 0)
            {
                float vect1 = 3.75f;
                float vect2 = 3.75f;

                for (int i = 0; i < 4; i++)
                {
                    // Random upward vector.
                    Vector2 launchVelocity = new Vector2(vect1,vect2);
                    // Importantly, ai1 is set to 1 here. This is checked in OnTileCollide to prevent bouncing and here in Kill to prevent an infinite chain of splitting projectiles.
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, launchVelocity, ModContent.ProjectileType<FrostflakeShard>(), Projectile.damage/2, Projectile.knockBack/3, Main.myPlayer, 0, 1);

                    if (i == 1)
                    {
                        vect1 = -3.75f;
                        vect2 = 3.75f;
                    }
                    else if (i == 2)
                    {
                        vect1 = 3.75f;
                        vect2 = -3.75f;
                    }
                    else
                    {
                        vect1 = -3.75f;
                        vect2 = -3.75f;
                    }
                }
            }

            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.penetrate == 2)
            {
                target.AddBuff(BuffID.Frostburn2, 300);
            }
        }
    }
}
