using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Items.Armor;
using NewLoot.Common.Players;
using System;
using NewLoot.Content.Buffs;
using System.Threading;
using Terraria.Audio;
using NewLoot.Content.Items.Accessories;

namespace NewLoot.Common.Global
{
    internal class RepeaterGlobal : GlobalItem
    {
        public static int RepeaterUpdate(Player player, int repeaterShots, int repeaterMaxShots, int heatRegenRate, int count)
        {
            if (player.GetModPlayer<CoolingPackPlayer>().coolingPackEquipped == true)
            {
                heatRegenRate = (int)(heatRegenRate * 0.8f);
            }

            if (player.GetModPlayer<OverheatPlayer>().overheated == true)
            {

                if (Main.rand.Next(1, 3) == 1)
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Lava, 0f, 0f, 100, default, 2f);
                    dust.scale = 0.75f;
                    dust.velocity *= 0.3f;
                    dust.noGravity = true;
                }
                else
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                    dust.scale = 1f;
                    dust.velocity *= 0.3f;
                    dust.noGravity = true;
                }

            }

            if (count >= heatRegenRate)
            {
                if (repeaterShots % repeaterMaxShots > 0 && player.releaseUseItem == true)
                {
                    repeaterShots -= 1;
                }
            }

            return repeaterShots;
        }


        public static int CountUpdate(Player player, int heatRegenRate, int count)
        {

            if (player.GetModPlayer<CoolingPackPlayer>().coolingPackEquipped == true)
            {
                heatRegenRate = (int)(heatRegenRate * 0.8f);
            }


            if (player.GetModPlayer<OverheatPlayer>().overheated == false)
            {
                count++;
            }
            else
            {
                count = 0;
            }
            if (count > heatRegenRate)
            {
                count = 0;
            }
            return count;
        }



        public static int RepeaterUse(Player player, int repeaterShots, int repeaterMaxShots, int overheatTime, int shotIncrease, bool isRageRepeater)
        {

            repeaterShots += shotIncrease;

            if (repeaterShots % repeaterMaxShots == 0)
            {
                player.AddBuff(ModContent.BuffType<Overheat>(), overheatTime);

                if (isRageRepeater == true)
                {
                    player.AddBuff(BuffID.Rage, overheatTime);
                }
            }

            return repeaterShots;
        }
    }
}
