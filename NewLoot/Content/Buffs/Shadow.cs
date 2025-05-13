using Humanizer;
using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class Shadow : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShadowPlayer>().equipped = true;
        }

    }

    internal class ShadowPlayer : ModPlayer
    {
        public bool equipped;
        private int timer;
        public override void UpdateEquips()
        {
            if (equipped)
            {
                Player.shadowDodge = true;
            }
            else
            {
                Player.shadowDodge = false;
            }
        }
        public override void ResetEffects()
        {
            MakeSureEquipWork();
        }

        public int MakeSureEquipWork()
        {
            if (timer == 0)
            {
                timer++;
            }
            else
            {
                timer = 0;
                equipped = false;
            }
            return timer;
        }
    }

}
