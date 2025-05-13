using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Newloot.Content.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using NewLoot.Content.Projectiles;
using System.Runtime.InteropServices;

namespace Newloot.Content.Projectiles
{
    internal class ChaosBoomerangProj : ModProjectile
    {
        public override string Texture => "NewLoot/Content/Items/Weapons/ChaosBoomerang";

        public static Vector2 boomerangPos;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 32;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            boomerangPos = Projectile.Center;
        }


    }
}
