using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Security.Cryptography.X509Certificates;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Projectiles
{
    internal class IceField : ModProjectile
    {
        private int timer = 0;

        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1500;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.scale = 6;
            DrawOriginOffsetX = -25;
            DrawOriginOffsetY = 125;
        }

        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 185;


            SimpleCustomCollisionLogic(Projectile.getRect(), 1);
            Visuals();

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
                    Main.player[index].statDefense += 8;
                    //dict.Remove(type);
                }
            }
        }

        private void Visuals()
        {

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 2;
            timer++;
            if (timer >= 12)
            {
                Projectile.frameCounter++;

                if (Projectile.frameCounter >= frameSpeed)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;

                    if (Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 0;
                        timer = 0;
                    }
                }
            }


            // Some visuals here
            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 0.5f);
        }

    }
}
