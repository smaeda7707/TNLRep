using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class Snowflake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 0;

            Projectile.penetrate = 3;
            Projectile.timeLeft = 45;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 30;// the cooldown between hits (For local NPC i frames)

        }
        public override void AI()
        {
            float rotateSpeed = 0.5f * Projectile.direction;
            Projectile.rotation += rotateSpeed;
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
                int rotateNum = 360 / 8;
                const int NumProjectiles = 8;
                for (int i = 0; i < NumProjectiles; i++)
                {
                    // Rotate the velocity randomly by 30 degrees at max.
                    Vector2 newVelocity = (Projectile.velocity/2).RotatedBy(MathHelper.ToRadians(rotateNum));

                    // Create a projectile.
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, newVelocity, ModContent.ProjectileType<SnowflakeShard>(), Projectile.damage / 3, Projectile.knockBack / 3, Main.myPlayer, 0, 1);
                    rotateNum += 360 / 8;
                }
            }

            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}
