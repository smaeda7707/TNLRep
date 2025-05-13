using ExampleMod.Common.GlobalNPCs;
using Terraria;
using Terraria.ModLoader;

namespace Newloot.Content.Buffs
{
    public class HoneyDiscDebuff : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DOTGlobalNPC>().honeyDiscDebuff = true;
        }
    }
}
