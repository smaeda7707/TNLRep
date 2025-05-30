﻿using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using NewLoot.Content.Buffs;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class Bubble : ModProjectile
    {
        private int timer;
        public override void SetDefaults()
        {
            Projectile.height = 34;
            Projectile.width = 34;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.friendly = false; // Can the projectile deal damage to enemies?

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;

            // These help the projectile hitbox be centered on the projectile sprite.

        }
        public override void AI()
        {
            Projectile.alpha = 110;
            Projectile.timeLeft = 10;

            if (BubbleYoyoProj.mode == 0)
            {
                Projectile.Kill();
            }
            else if (BubbleYoyoProj.mode == 2)
            {
                Projectile.scale = 1.5f;
            }
            else if (BubbleYoyoProj.mode == 3)
            {
                Projectile.scale = 2.25f;
            }
            else if (BubbleYoyoProj.mode == 4)
            {
                Projectile.scale = 3.375f;
                timer++;
            }

            if (timer == 15)
            {
                Projectile.friendly = true;
                Projectile.alpha = 255;
                SoundEngine.PlaySound(SoundID.Item95, Projectile.position);

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, 40, 40, DustID.Water, 0f, 0f, 100, default, 2f);
                    dust.velocity *= 9.4f;
                    dust.noGravity = true;
                }
            }
            if (timer > 15)
            {
                Projectile.alpha = 255;
            }
            if (timer >= 18)
            {
                Projectile.Kill();
            }

            Projectile.Center = BubbleYoyoProj.bubbleYoyoPos;
        }
        public override void OnKill(int timeLeft)
        {
            timer = 0;
            BubbleYoyoProj.mode = 0;
            
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (BubbleYoyoProj.mode == 4)
            {
                hitbox.Width = 115;
                hitbox.Height = 115;
                hitbox.Offset(-40, -40); // Offset the hitbox to center it (Calculated by ((damageHitbox - normalHeight or Width) /2)   )
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage /= 2;
        }

    }
}
