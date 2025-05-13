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
    internal class CritDamageGlobal : GlobalItem
    {
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.GetModPlayer<IceTrinketPlayer>().iceTrinketEquipped)
            {
                modifiers.CritDamage *= 1.12f;
            }
            if (player.GetModPlayer<KanumGreavesPlayer>().equipped)
            {
                modifiers.CritDamage *= 1.05f;
            }
            if (player.GetModPlayer<BloodChestplatePlayer>().equipped)
            {
                modifiers.CritDamage *= 1.07f;
            }
        }
    }
    internal class CritDamageGlobalProj : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.player[projectile.owner].GetModPlayer<IceTrinketPlayer>().iceTrinketEquipped)
            {
                modifiers.CritDamage *= 1.12f;
            }
            if (Main.player[projectile.owner].GetModPlayer<KanumGreavesPlayer>().equipped)
            {
                modifiers.CritDamage *= 1.05f;
            }
            if (Main.player[projectile.owner].GetModPlayer<BloodChestplatePlayer>().equipped)
            {
                modifiers.CritDamage *= 1.07f;
            }
        }
    }
}
