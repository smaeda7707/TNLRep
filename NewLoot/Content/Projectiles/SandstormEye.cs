using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Projectiles;

namespace Newloot.Content.Projectiles
{
    internal class SandstormEye : ModProjectile
    {

        public override void SetDefaults()
        {
            // While the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;

            // 5 second fuse.
            Projectile.timeLeft = 60;
            Projectile.width = 38;
            Projectile.height = 38;
        }

        // The projectile is very bouncy, but the spawned children projectiles shouldn't bounce at all.


        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f)
            {
                Projectile.ai[0] = 10f;
                // Roll speed dampening. 
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;

                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                // Delayed gravity
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
            // Rotation increased by velocity.X 
            Projectile.rotation += Projectile.velocity.X * 0.5f;
        }

        public override void OnKill(int timeLeft)
        {
            // If we are the original projectile running on the owner, spawn the 5 child projectiles.
            if (Projectile.owner == Main.myPlayer && Projectile.ai[1] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y - 1f, Main.rand.Next(0, 0) * 1f, Main.rand.Next(0, 0) * 1f, ModContent.ProjectileType<Sandstorm>(), (int)(Projectile.damage /3), 0, Projectile.owner);
            }
        }
    }
}
