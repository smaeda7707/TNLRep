using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Projectiles
{
    internal class CoriteSawblade : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.height = 60;
            Projectile.width = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.friendly = true; // Can the projectile deal damage to enemies?

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;

            // These help the projectile hitbox be centered on the projectile sprite.

        }
        public override void AI()
        {

            float rotateSpeed = 0.6f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            if (CoriteYoyoProj.mode == 0 || !Main.player[Projectile.owner].HasBuff(ModContent.BuffType<CoriteYoyoBuff>()))
            {
                Projectile.Kill();
            }

            Projectile.Center = CoriteYoyoProj.coriteYoyoPos;
        }

    }
}
