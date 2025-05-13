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
    internal class SunOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;

            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 310)
            {
                Projectile.velocity *= 0.2f;
            }
            Color color = new Color(255, 237, 114);

            float rotateSpeed = 0.03f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Lighting.AddLight(Projectile.Center, color.ToVector3() * 0.5f);

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightYellow;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localNPCHitCooldown <= 3)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.localNPCHitCooldown > 20)
                {
                    Projectile.localNPCHitCooldown -= 5;
                }
                else
                {
                    Projectile.localNPCHitCooldown -= 3;
                }
                
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.localNPCHitCooldown == 30)
            {
                modifiers.FinalDamage *= 1.25f;
                modifiers.Knockback *= 1.5f;
            }
        }

    }







    internal class SunOrbLarge : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = -1;

            Projectile.timeLeft = 1500;
        }
        public override void AI()
        {
            Color color = new Color(255, 237, 114);

            float rotateSpeed = 0.015f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Lighting.AddLight(Projectile.Center, color.ToVector3() * 0.5f);
            SimpleCustomCollisionLogic(Projectile.Hitbox, 0);

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightYellow;
        }

        public void SimpleCustomCollisionLogic(Rectangle projRect, int allowed)
        {
            //var type = member.Key;
            //var time = member.Value.Time;

            //allowed = 1 for only players, 2 for only npc's, 0 for both
            if (allowed != 1)
            {
                int index = Array.FindIndex(Main.npc, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    Main.npc[index].AddBuff(BuffID.OnFire3, 300);
                    //dict.Remove(type);
                }
            }
            if (allowed != 2)
            {
                int index = Array.FindIndex(Main.player, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    Main.player[index].AddBuff(ModContent.BuffType<Sunlight>(), 120);
                    //dict.Remove(type);
                }
            }
        }

    }
}
