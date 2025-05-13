using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Content.Buffs;
using System.Threading;
using rail;

namespace NewLoot.Content.Projectiles
{
    internal class MechaMushroom : ModProjectile
    {
        private int timer;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.12f;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.98f;
            int range = 250;
            timer++;

            if (timer% 145 == 0)
            {
                Projectile.NewProjectileDirect(Main.player[Projectile.owner].GetSource_FromThis(), Projectile.Center + new Vector2(Main.rand.Next(-range, range), Main.rand.Next(-range, range)), Vector2.Zero, ProjectileID.TruffleSpore, Projectile.damage * 2, 0);
            }
            Visuals();
        }

        private void Visuals()
        {
            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 4;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            Color color = new Color(0, 221, 16);
            if (Projectile.frame == 1)
            {
                color = new Color(115, 231, 0);
            }
            else if (Projectile.frame == 2)
            {
                color = new Color(174, 242, 0);
            }

            Lighting.AddLight(Projectile.Center, color.ToVector3() * 0.5f);
        }

    }
}
