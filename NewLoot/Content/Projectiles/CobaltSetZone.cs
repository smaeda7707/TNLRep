using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Content.Buffs;
using NewLoot.Content.Items.Armor;

namespace NewLoot.Content.Projectiles
{
    internal class CobaltSetZone : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 900;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 185;
            Projectile.Center = Main.player[Projectile.owner].Center;

            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.active && proj.hostile && Projectile.Colliding(Projectile.getRect(), proj.getRect()) && proj.GetGlobalProjectile<CobaltSetProjGlobal>().cobaltTimer > 0)
                {
                    proj.velocity *= 0.98f;
                    proj.GetGlobalProjectile<CobaltSetProjGlobal>().cobaltTimer--;
                }
            }
        }

    }

    internal class CobaltSetProjGlobal : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public int cobaltTimer = 20;

    }
}
