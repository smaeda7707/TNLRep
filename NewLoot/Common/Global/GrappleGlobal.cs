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
    internal class GrappleGlobal : GlobalProjectile
    {
        public override void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
        {
            if (player.GetModPlayer<JungleTrinketPlayer>().jungleTrinketEquipped)
            {
                speed *= 1.12f;
            }

        }

    }
}
