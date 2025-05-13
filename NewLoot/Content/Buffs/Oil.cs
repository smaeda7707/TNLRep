
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewLoot.Common.Global;

namespace NewLoot.Content.Buffs
{
    internal class Oil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.HasBuff(ModContent.BuffType<Flammable>()))
            {
                Projectile.NewProjectile(npc.GetGlobalNPC<GlobalNPCFields>().oilPlayer.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<OilExplosionBig>(), 140, 0f);
                npc.DelBuff(Type);
            }
            else if (npc.HasBuff(BuffID.OnFire) || npc.HasBuff(BuffID.OnFire3))
            {
                Projectile.NewProjectile(npc.GetGlobalNPC<GlobalNPCFields>().oilPlayer.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<OilExplosion>(), 80, 0f);
                npc.DelBuff(Type);
            }
        }
    }

}
