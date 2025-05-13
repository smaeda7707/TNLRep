
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Buffs
{
    internal class ModernBowBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 1079)
            {
                Projectile.NewProjectileDirect(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<ModernBowTarget>(), 0, 0, player.whoAmI);
            }
        }
    }

}
