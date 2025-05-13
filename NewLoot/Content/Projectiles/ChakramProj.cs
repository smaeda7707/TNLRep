using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Newloot.Content.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using NewLoot.Content.Projectiles;
using System.Runtime.InteropServices;

namespace Newloot.Content.Projectiles
{
    internal class ChakramProj : ModProjectile
    {
        public override string Texture => "NewLoot/Content/Items/Weapons/Chakram";

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
        }


    }

    internal class ChakramProjSecondary : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.localNPCHitCooldown = 2;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 120 && Projectile.timeLeft >= 116)
            {
                Projectile.usesLocalNPCImmunity = true;
            }
            else
            {
                Projectile.usesLocalNPCImmunity = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft > 120)
            {
                Projectile.velocity *= -0.9f;
                Projectile.timeLeft = 120;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.timeLeft <= 120)
            {
                modifiers.Knockback *= 1.8f;

                // Make knockback go towords projectile center
                modifiers.HitDirectionOverride = target.position.X > Main.player[Projectile.owner].MountedCenter.X ? -1 : 1;
            }
        }

    }
}
