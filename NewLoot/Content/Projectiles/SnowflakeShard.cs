using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

namespace NewLoot.Content.Projectiles
{
    internal class SnowflakeShard : ModProjectile
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
            Projectile.extraUpdates = 0;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 25;
            Projectile.scale = 1f;

            Projectile.usesLocalNPCImmunity = true;// Gives projectile it's own i frames so other projectiles can hit along side it
            Projectile.localNPCHitCooldown = 30;// the cooldown between hits (For local NPC i frames)
            Projectile.ArmorPenetration = 15;
        }
        public override void AI()
        {
            float rotateSpeed = 0.5f * Projectile.direction;
            Projectile.rotation += rotateSpeed;
        }

        public override void OnKill(int timeLeft)
        {
            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}
