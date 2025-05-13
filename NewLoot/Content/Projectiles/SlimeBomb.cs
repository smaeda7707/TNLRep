using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NewLoot.Content.Projectiles;
using Newloot.Content.Buffs;
using NewLoot.Common.Global;

namespace NewLoot.Content.Projectiles
{
    internal class SlimeBomb : ModProjectile
    {
        private const int DefaultWidthHeight = 16;
        private bool bounced = false;
        private int originalDamage;


        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 3f;
            set => Projectile.ai[0] = value ? 3f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public float StickTimer
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = DefaultWidthHeight; // The width of projectile hitbox
            Projectile.height = DefaultWidthHeight; // The height of projectile hitbox
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Generic; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            //Projectile.extraUpdates = 1;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 120)
            {
                originalDamage = Projectile.damage;
            }
            if (IsStickingToTarget)
            {
                StickyAI();
            }
            else if (Projectile.timeLeft <= 3)
            {
                ExplodingAI();
            }
            else
            {
                NormalAI();
            }

            if (Projectile.timeLeft == 3)
            {
                
                IsStickingToTarget = false;
                SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
                SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
                Projectile.Resize(80, 80);
            }
        }

        // The projectile is very bouncy, but the spawned children projectiles shouldn't bounce at all.
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (IsStickingToTarget == false && Projectile.timeLeft > 3)
            {
                // OnTileCollide can trigger quite frequently, so using soundDelay helps prevent the sound from overlapping too much.
                if (bounced == false)
                {
                    Projectile.damage = (int)(Projectile.damage * 1.15f);
                    bounced = true;
                }
                // This code makes the projectile very bouncy.
                if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                {
                    Projectile.velocity.X = oldVelocity.X * -0.8f;
                }
                if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                {
                    Projectile.velocity.Y = oldVelocity.Y * -0.8f;
                }
            }
            return false;
        }


        private const int MaxStickingJavelin = 10; // This is the max amount of javelins able to be attached to a single NPC
        private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin]; // The point array holding for sticking javelins
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft > 3)
            {
                IsStickingToTarget = true; // we are sticking to a target
                TargetWhoAmI = target.whoAmI; // Set the target whoAmI
                Projectile.velocity = (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this javelin
                Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore


                // KillOldestJavelin will kill the oldest projectile stuck to the specified npc.
                // It only works if ai[0] is 1 when sticking and ai[1] is the target npc index, which is what IsStickingToTarget and TargetWhoAmI correspond to.
                Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingJavelins);
            }
            else
            {
                Projectile.damage = (int)(Projectile.damage * 0.65f);
            }
        }

        private const int StickTime = 60 * 8; // 8 seconds
        private void StickyAI()
        {
            Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            StickTimer += 1f;

            int npcTarget = TargetWhoAmI;
            if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
            { // If the index is past its limits, kill it
                IsStickingToTarget = false;
                Projectile.timeLeft = 3;
            }
            else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
            {
                // If the target is active and can take damage
                // Set the projectile's position relative to the target's center
                Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
            }
            else
            { // Otherwise, kill the projectile
                IsStickingToTarget = false;
                Projectile.timeLeft = 3;
            }
        }

        private void ExplodingAI()
        {
            Projectile.alpha = 255;
            Color slimeColor = new Color(176, 206, 255);
            Projectile.damage = originalDamage;

            for (int i = 0; i < 32; i++)
            {
                float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                // Original Excalibur color: Color.Gold, Color.White
                Color dustColor = Color.Lerp(slimeColor, slimeColor, 0.2f);
                Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 70f * Projectile.scale * Projectile.scale), DustID.t_Slime, dustVelocity * 1f, 100, dustColor, 0.4f);
                coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                coloredDust.scale = 1.25f;
                coloredDust.noGravity = true;

            }
        }

        private void NormalAI()
        {
            Projectile.alpha = 28;

            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
    }






















    internal class BigSlimeBomb : ModProjectile
    {
        private const int DefaultWidthHeight = 26;
        private bool bounced = false;
        private int originalDamage;


        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 3f;
            set => Projectile.ai[0] = value ? 3f : 0f;
        }

        // Index of the current target
        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public float StickTimer
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = DefaultWidthHeight; // The width of projectile hitbox
            Projectile.height = DefaultWidthHeight; // The height of projectile hitbox
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Generic; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            //Projectile.extraUpdates = 1;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 120)
            {
                originalDamage = Projectile.damage;
            }
            if (IsStickingToTarget)
            {
                StickyAI();
            }
            else if (Projectile.timeLeft <= 3)
            {
                ExplodingAI();
            }
            else
            {
                NormalAI();
            }

            if (Projectile.timeLeft == 3)
            {
                Projectile.damage = originalDamage;
                Vector2 velocity = new Vector2(5, 5);

                IsStickingToTarget = false;
                SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
                SoundEngine.PlaySound(SoundID.NPCDeath64, Projectile.Center);
                Projectile.Resize(80, 80);
                GlobalProjectileMethods.CreateRotatedProjectiles(Main.player[Projectile.owner], Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<SlimeBall>(), Projectile.damage / 4, 45, 8, Projectile.knockBack / 2, false);
            }
        }

        // The projectile is very bouncy, but the spawned children projectiles shouldn't bounce at all.
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (IsStickingToTarget == false && Projectile.timeLeft > 3)
            {
                // OnTileCollide can trigger quite frequently, so using soundDelay helps prevent the sound from overlapping too much.
                if (bounced == false)
                {
                    Projectile.damage = (int)(Projectile.damage * 1.15f);
                    bounced = true;
                }
                // This code makes the projectile very bouncy.
                if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                {
                    Projectile.velocity.X = oldVelocity.X * -0.8f;
                }
                if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                {
                    Projectile.velocity.Y = oldVelocity.Y * -0.8f;
                }
            }
            return false;
        }


        private const int MaxStickingJavelin = 10; // This is the max amount of javelins able to be attached to a single NPC
        private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin]; // The point array holding for sticking javelins
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft > 3)
            {
                IsStickingToTarget = true; // we are sticking to a target
                TargetWhoAmI = target.whoAmI; // Set the target whoAmI
                Projectile.velocity = (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this javelin
                Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore


                // KillOldestJavelin will kill the oldest projectile stuck to the specified npc.
                // It only works if ai[0] is 1 when sticking and ai[1] is the target npc index, which is what IsStickingToTarget and TargetWhoAmI correspond to.
                Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingJavelins);
            }
            else
            {
                Projectile.damage = (int)(Projectile.damage * 0.65f);
            }
        }

        private const int StickTime = 60 * 8; // 8 seconds
        private void StickyAI()
        {
            Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            StickTimer += 1f;

            int npcTarget = TargetWhoAmI;
            if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
            { // If the index is past its limits, kill it
                IsStickingToTarget = false;
                Projectile.timeLeft = 3;
            }
            else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
            {
                // If the target is active and can take damage
                // Set the projectile's position relative to the target's center
                Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
            }
            else
            { // Otherwise, kill the projectile
                IsStickingToTarget = false;
                Projectile.timeLeft = 3;
            }
        }

        private void ExplodingAI()
        {
            Projectile.alpha = 255;
            Color slimeColor = new Color(176, 206, 255);

            for (int i = 0; i < 32; i++)
            {
                float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
                Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

                // Original Excalibur color: Color.Gold, Color.White
                Color dustColor = Color.Lerp(slimeColor, slimeColor, 0.2f);
                Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 70f * Projectile.scale * Projectile.scale), DustID.t_Slime, dustVelocity * 1f, 100, dustColor, 0.4f);
                coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                coloredDust.scale = 1.25f;
                coloredDust.noGravity = true;

            }
        }

        private void NormalAI()
        {
            Projectile.alpha = 28;

            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
    }
}
