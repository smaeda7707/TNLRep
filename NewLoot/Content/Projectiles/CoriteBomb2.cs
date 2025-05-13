using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using NewLoot.Content.Buffs;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class CoriteBomb2 : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.height = 62;
            Projectile.width = 62;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 30;
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = false;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;


        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
                
        }
        public override void AI()
        {

            if (Projectile.timeLeft == 30)
            {
                Projectile.friendly = true;

                // Play explosion sound
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                // Smoke Dust spawn
                for (int i = 0; i < 40; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.AncientLight, 0f, 0f, 100, default, 2f);
                    dust.velocity *= 1.8f;
                    dust.noGravity = true;
                }
            }
            else
            {
                Projectile.friendly = false;
            }

        }

    }
}
