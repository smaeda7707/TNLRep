using Microsoft.Xna.Framework.Graphics;
using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewLoot.Common.Players;

namespace NewLoot.Content.Buffs
{
    internal class SubZeroBuff : ModBuff
    {
       
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SubZeroBuffPlayer>().subZerod = true;
            player.GetCritChance(DamageClass.Generic) += 15;
            player.endurance += 0.10f;
            player.frostArmor = true;

            player.GetModPlayer<Ultimate>().ultimateRegenRate = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Vector2 shake = new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3));

            drawParams.Position += shake;
            drawParams.TextPosition += shake;

            return true;
        }

    }
    public class SubZeroBuffPlayer : ModPlayer
    {
        public bool subZerod;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            subZerod = false;
        }
    }
}
