using NewLoot.Content.Items.Armor;
using NewLoot.Content.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class Overheat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OverheatPlayer>().overheated = true;
        }

    }
    public class OverheatPlayer : ModPlayer
    {
        public bool overheated;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            overheated = false;
        }
    }
}
