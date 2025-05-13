
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewLoot.Content.Buffs
{
    internal class Flammable : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
        }
    }

}
