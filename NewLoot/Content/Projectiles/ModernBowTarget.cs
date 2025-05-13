using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Content.Buffs;
using NewLoot.Common.Players;
using NewLoot.Common.Global;

namespace NewLoot.Content.Projectiles
{
    internal class ModernBowTarget : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.timeLeft = 2;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 230;
            Projectile.Center = Main.MouseWorld;

            if (Main.player[Projectile.owner].HasBuff(ModContent.BuffType<ModernBowBuff>())) {
                Projectile.timeLeft = 2;
            }

        }

    }
}
