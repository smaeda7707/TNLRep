using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace NewLoot.Content.Projectiles
{
    internal class ThornRing : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.height = 120;
            Projectile.width = 120;
            Projectile.penetrate = 20;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void AI()
        {

            float rotateSpeed = 0.1f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Projectile.Center = Main.player[Projectile.owner].Center;
            

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 300);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Make knockback go away from player
            modifiers.HitDirectionOverride = target.position.X > Main.player[Projectile.owner].MountedCenter.X ? 1 : -1;

        }
    }
}
