using Terraria;
using Terraria.ModLoader;

namespace NewLoot.Content.Projectiles
{
    internal class Null : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.timeLeft = 1;
        }
        public override void AI()
        {
            // Set to transparent. This projectile technically lives as transparent for about 3 frames
            Projectile.alpha = 2;
        }
    }
}
