using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NewLoot.Content.Projectiles;
using NewLoot.Content.Buffs;
using NewLoot.Common.Global;

namespace NewLoot.Content.Projectiles
{
    internal class FlammableSpark : ModProjectile
    {
        private const int DefaultWidthHeight = 6;
        public override string Texture => "NewLoot/Content/Projectiles/Spark";
        public override void SetDefaults()
        {
            Projectile.width = DefaultWidthHeight; // The width of projectile hitbox
            Projectile.height = DefaultWidthHeight; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 30; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

        }
        public override void AI()
        {
            Projectile.alpha = 255;

            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

            // Original Excalibur color: Color.Gold, Color.White
            Color dustColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat() * 0.3f);
            Dust coloredDust = Dust.NewDustPerfect(Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 0.1f * Projectile.scale + 1f * Projectile.scale), DustID.Lava, dustVelocity * 1f, 100, dustColor, 0.4f);
            coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
            coloredDust.scale = 1f;
            coloredDust.noGravity = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Flammable>(), 300);
            target.GetGlobalNPC<GlobalNPCFields>().oilPlayer = Main.player[Projectile.owner];
        }

    }
}
