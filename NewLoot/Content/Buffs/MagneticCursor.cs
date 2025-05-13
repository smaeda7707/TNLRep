using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class MagneticCursor : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MagneticCursorPlayer>().cursorMagnet = true;
        }

    }
    public class MagneticCursorPlayer : ModPlayer
    {
        public bool cursorMagnet;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            cursorMagnet = false;
        }
    }
}
