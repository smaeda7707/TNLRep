using ExampleMod.Common.GlobalNPCs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewLoot.Content.Projectiles;

namespace Newloot.Content.Buffs
{
    public class StingerBoltDebuff : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DOTGlobalNPC>().stingerBoltDebuff = true;
        }
    }

    public class StingerBoltItem : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit && target.HasBuff(ModContent.BuffType<StingerBoltDebuff>()))
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PoisonCloud>(), 15, 0);
            }
        }
    }

    public class StingerBoltGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit && target.HasBuff(ModContent.BuffType<StingerBoltDebuff>()) && projectile.type != ModContent.ProjectileType<PoisonCloud>())
            {
                Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PoisonCloud>(), 15, 0);
            }
        }
    }
}
