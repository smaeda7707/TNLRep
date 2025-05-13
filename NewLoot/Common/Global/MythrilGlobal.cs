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
using static NewLoot.Content.Items.Armor.MythrilHelm;

namespace NewLoot.Common.Global
{
    internal class MythrilGlobal : GlobalItem
    {

        public override void SetDefaults(Item item)
        {
            item.GetGlobalItem<GlobalFields>().initDamage = item.damage;

            item.GetGlobalItem<GlobalFields>().originalClass = item.DamageType;
        }


        public override void UpdateInventory(Item item, Player player)
        {
            if (player.GetModPlayer<MythrilPlayer>().setBonusOn)
            {
                if (item.DamageType != DamageClass.Magic)
                {
                    item.mana = item.GetGlobalItem<GlobalFields>().initDamage / 6;
                    item.DamageType = DamageClass.Magic;
                    if (item.mana < 4)
                    {
                        item.mana = 4;
                    }
                }
            }
            else
            {
                if (item.GetGlobalItem<GlobalFields>().originalClass != DamageClass.Magic)
                {
                    item.DamageType = item.GetGlobalItem<GlobalFields>().originalClass;
                    item.mana = 0;
                }
            }
            base.UpdateInventory(item, player);
        }
    }


    internal class MythrilGlobalProjectile : GlobalProjectile
    {

        public override void SetDefaults(Projectile projectile)
        {
            projectile.GetGlobalProjectile<GlobalProjectileFields>().originalClass = projectile.DamageType;
        }


        public override void AI(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<MythrilPlayer>().setBonusOn)
            {
                if (projectile.DamageType != DamageClass.Magic && projectile.friendly)
                {
                    projectile.DamageType = DamageClass.Magic;
                }
            }
            else
            {
                projectile.DamageType = projectile.GetGlobalProjectile<GlobalProjectileFields>().originalClass;
            }
        }
    }
}
