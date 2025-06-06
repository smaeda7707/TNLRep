﻿using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Projectiles
{
    internal class FrostflakeShard : ModProjectile
    {
        public static int realTimeLeft = 25;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 0;

            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 30;// the cooldown between hits (For local NPC i frames)
            Projectile.ArmorPenetration = 15;

            Projectile.penetrate = 1;
            Projectile.timeLeft = realTimeLeft;

        }
        public override void AI()
        {
            float rotateSpeed = 0.5f * Projectile.direction;
            Projectile.rotation += rotateSpeed;

            Projectile.alpha = 100;

            if (Main.player[Projectile.owner].GetModPlayer<FrostRodBuffPlayer>().boomerangShards == true)
            {
                Projectile.penetrate = 2;
                realTimeLeft = 50;

                if (Projectile.timeLeft == 25)
                {
                    Projectile.velocity *= -1.5f;
                }
            }
            else
            {
                realTimeLeft = 25;
                Projectile.penetrate = 1;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void OnKill(int timeLeft)
        {
            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}
