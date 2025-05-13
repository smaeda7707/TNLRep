using NewLoot.Common.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;

namespace NewLoot.Common.Players
{
    internal class NewLootPlayer : ModPlayer
    {
        public int adamantiteSetTimer;
        public int tinWhipbuffTimer;
        public override void PreUpdate()
        {
            Player.maxRunSpeed += 0.15f;

        }
    }
}
