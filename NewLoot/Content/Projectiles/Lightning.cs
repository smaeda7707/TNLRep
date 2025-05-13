using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;

namespace NewLoot.Content.Projectiles
{
    internal class Lightning : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.ArmorPenetration = 50;
            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 10;// the cooldown between hits (For local NPC i frames)
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 255;
        }
    }
}
