using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Common.Global
{
    internal class GlobalItemList : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.PoisonDart)
            {
                item.damage = 6;
            }
        }
    }
}
