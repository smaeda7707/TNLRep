using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System;

namespace NewLoot.Content.Projectiles
{
    internal class SnowWall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 2;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.scale = 2f;




        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
           modifiers.Knockback += 15;
        }
        public override void AI()
        {

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f,
                        40, default, 1f);
                }

            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = 112;
            hitbox.Height = 90;

            hitbox.Offset(0, -34);
        }
    }
}
