using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class AquaBuff : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AquaBuffPlayer>().aquaBuffed = true;
        }

    }
    public class AquaBuffPlayer : ModPlayer
    {
        public bool aquaBuffed;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            aquaBuffed = false;
        }
    }
}
