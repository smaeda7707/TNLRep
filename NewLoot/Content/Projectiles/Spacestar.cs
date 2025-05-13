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
    internal class Spacestar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft <= 540)
            {
                return Color.White;
            }
            else
            {
                return base.GetAlpha(lightColor);
            }
        }
        public override void AI()
        {

            Projectile.velocity *= 0.9f;
            Projectile.alpha = 100;

            
            if (Projectile.timeLeft <= 540)
            {
                SimpleCustomCollisionLogic(Projectile.getRect(), 1);
            }
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
                    //Main.npc[index].AddBuff(type, time);
                    //dict.Remove(type);
                }
            }
            if (allowed != 2)
            {
                int index = Array.FindIndex(Main.player, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    Main.player[index].statMana += 40;
                    Projectile.Kill();
                    //dict.Remove(type);
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();
            Color dustColor = Color.Lerp(Color.LightBlue, Color.Navy, Main.rand.NextFloat() * 0.3f);

            Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 5f * Projectile.scale), DustID.AncientLight, dustVelocity * 1f, 100, dustColor, 0.4f);
            coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
            coloredDust.scale = 2f;
            coloredDust.noGravity = true;
        }

    }
}
