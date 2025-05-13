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
    internal class SandGust : ModProjectile
    {
        private int stage;
        private int stageCharge = 0;
        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 390;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;

            Projectile.timeLeft = 3;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner]; // Get the owner of the projectile.
            Projectile.direction = owner.direction; // Direction will be -1 when facing left and +1 when facing right. 
            owner.heldProj = Projectile.whoAmI; // Set the owner's held projectile to this projectile. heldProj is used so that the projectile will be killed when the player drops or swap items.

            Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
            Projectile.velocity = vectorToCursor/1000;

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            }
            Projectile.Center = owner.Center;

            Projectile.alpha = 155;
            Animation();



            if (owner.channel)
            {
                Projectile.timeLeft = 3;
                stageCharge++;
            }

            if (stageCharge == 300 || stageCharge == 600)
            {
                stage++;
            }

            if (stage == 1)
            {
                Projectile.localNPCHitCooldown = 20;
            }
            if (stage == 2)
            {
                Projectile.localNPCHitCooldown = 10;
            }



            owner.itemAnimation = owner.itemAnimationMax;
            owner.itemTime = owner.itemTimeMax;
        }

        private void Animation()
        {
            int frameSpeed;
            if (stage == 0)
            {
               frameSpeed = 8;
            }
            else if (stage == 1)
            {
                frameSpeed = 5;
            }
            else
            {
               frameSpeed = 2;
            }

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }


    }

}
