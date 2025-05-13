using Humanizer;
using Microsoft.Xna.Framework;
using NewLoot.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewLoot.Content.Buffs
{
    internal class Halo : ModBuff
    {

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 1)
            {
                player.statLife += 80;

                float dustRotation = 360 + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 2f;
                Vector2 dustPosition = player.Top + dustRotation.ToRotationVector2() * 84f * 10;
                Vector2 dustVelocity = (dustRotation * MathHelper.PiOver2).ToRotationVector2();

                // Original Excalibur color: Color.Gold, Color.White
                Color dustColor = Color.Lerp(Color.Gold, Color.Gold, Main.rand.NextFloat() * 0.3f);

                for (int i = 0; i < 5; i++)
                {
                    Dust coloredDust = Dust.NewDustPerfect(player.Top + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 10f + 1f), DustID.AncientLight, dustVelocity * 1f, 100, dustColor, 0.4f);
                    coloredDust.fadeIn = 0.9f + Main.rand.NextFloat() * 0.15f;
                    coloredDust.scale = 1.3f;
                    coloredDust.noGravity = true;
                }

            }

        }
    }
}
