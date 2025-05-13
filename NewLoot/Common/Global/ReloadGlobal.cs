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

namespace NewLoot.Common.Global
{
    internal class ReloadGlobal : GlobalItem
    {
        public static int Reload(Player player, Item item, int gunLoad, int maxLoad, int reloadUseSpeed, int loadPerUse)
        {
            if (player.GetModPlayer<MagtaneVisorPlayer>().MagtaneVisorEquipped == true)
            {
                reloadUseSpeed -= 2;
            }
            item.useTime = reloadUseSpeed;
            item.useAnimation = reloadUseSpeed;

            if (gunLoad < maxLoad) // MAX LOAD
            {
                // The sound that this item plays when used.
                item.UseSound = SoundID.Item10;
                gunLoad += loadPerUse; // LOAD PER CLICK
            }

            return gunLoad;
        }
    }
}
