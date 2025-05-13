using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System;
using NewLoot.Content.Buffs;
using NewLoot.Content.Items.Weapons;

namespace NewLoot.Content.Projectiles
{
    internal class IceRapierSpin : ModProjectile
    {
        private float boost = 6f;
        private int immune;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 56;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Melee;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;

        }
        public override void AI()
        {
            Projectile.alpha = 60;
            Projectile.CritChance = 100;

            Player owner = Main.player[Projectile.owner];
            Projectile.Center = owner.Center;
            owner.velocity.Y = -boost; // Boost player up
            boost /= 1.02f;
            
            if (immune > 0)
            {
                owner.immuneTime = 5;
                owner.immune = true;
                immune--;
            }

            Animation();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 1.09f;
            if (Projectile.timeLeft > 15)
            {
                // Make knockback go towords projectile center
                modifiers.HitDirectionOverride = target.position.X > Projectile.Center.X ? -1 : 1;
                modifiers.DisableCrit();
            }
            else
            {
                modifiers.HitDirectionOverride = target.position.X > Projectile.Center.X ? 1 : -1;
            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            immune = 15;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = new Rectangle(0, Projectile.frame * 56, texture.Width, 56);

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(frame.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(1f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public void Animation()
        {
            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 1;

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
