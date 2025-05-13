using Newloot.Content.Buffs;
using Newloot.Content.Projectiles;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Common.GlobalNPCs
{
    internal class DOTGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool honeyDiscDebuff;
        public bool stingerBoltDebuff;

        public override void ResetEffects(NPC npc)
        {
            honeyDiscDebuff = false;
            stingerBoltDebuff = false;
        }

        public override void SetDefaults(NPC npc)
        {
            // TODO: This doesn't work currently. tModLoader needs to make a fix to allow changing buffImmune

            // We want our ExampleJavelin buff to follow the same immunities as BoneJavelin
            npc.buffImmune[ModContent.BuffType<HoneyDiscDebuff>()] = npc.buffImmune[BuffID.BoneJavelin];

            npc.buffImmune[ModContent.BuffType<StingerBoltDebuff>()] = npc.buffImmune[BuffID.BoneJavelin];
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (honeyDiscDebuff)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // Count how many ExampleJavelinProjectile are attached to this npc.
                int honeyDiscCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<HoneyDiscProjSecondary>() && p.ai[0] == 3f && p.ai[1] == npc.whoAmI)
                    {
                        honeyDiscCount++;
                    }
                }
                // Remember, lifeRegen affects the actual life loss, damage is just the text.
                // The logic shown here matches how vanilla debuffs stack in terms of damage numbers shown and actual life loss.
                npc.lifeRegen -= honeyDiscCount * 2 * 10;
                if (damage < honeyDiscCount * 10)
                {
                    damage = honeyDiscCount * 10;
                }
            }

            if (stingerBoltDebuff)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // Count how many ExampleJavelinProjectile are attached to this npc.
                int poisonBoltAmount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<StingerBolt>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        poisonBoltAmount++;
                    }
                }
                // Remember, lifeRegen affects the actual life loss, damage is just the text.
                // The logic shown here matches how vanilla debuffs stack in terms of damage numbers shown and actual life loss.
                npc.lifeRegen -= poisonBoltAmount * 2 * 8;
                if (damage < poisonBoltAmount * 8)
                {
                    damage = poisonBoltAmount * 8;
                }
            }
        }

    }
}
