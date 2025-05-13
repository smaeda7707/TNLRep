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
    internal class PoisonCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Ranged;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 45;
            Projectile.scale = 1.2f;

            DrawOriginOffsetX = 5;
            DrawOriginOffsetY = 5;


        }
        public override void AI()
        {
            Projectile.alpha = 130;

            
            Projectile.velocity *= 0.98f;

            Projectile.scale += 0.04f;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 360);

        }

    }
}
