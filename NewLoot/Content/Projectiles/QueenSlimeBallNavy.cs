using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NewLoot.Content.Projectiles;

namespace NewLoot.Content.Projectiles
{
    internal class QueenSlimeBallNavy : ModProjectile
    {
        private const int DefaultWidthHeight = 10;
        private bool bounced = false;

        public override void SetDefaults()
        {
            Projectile.width = DefaultWidthHeight; // The width of projectile hitbox
            Projectile.height = DefaultWidthHeight; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 180; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            //Projectile.extraUpdates = 1;
            Projectile.scale = 1.1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

            AIType = ProjectileID.WoodenArrowFriendly; // Act exactly like default Arrow
        }
        public override void AI()
        {
            Projectile.alpha = 40;
        }

        // The projectile is very bouncy, but the spawned children projectiles shouldn't bounce at all.
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // OnTileCollide can trigger quite frequently, so using soundDelay helps prevent the sound from overlapping too much.
            if (bounced == false) { 
                Projectile.damage = (int)(Projectile.damage * 1.5f);
                bounced = true;
            }
            // This code makes the projectile very bouncy.
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.8f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.8f;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            bounced = false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 1.1f;
        }
    }
}
