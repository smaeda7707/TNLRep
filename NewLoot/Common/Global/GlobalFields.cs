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
    internal class GlobalFields : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public DamageClass originalClass;

        public int initDamage;

        //Energy + Ultimate
        public int energyCost;


        // Ammo
        public int gunLoad;
        public int maxLoad;

    }

    internal class GlobalProjectileFields : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public DamageClass originalClass;

    }
    internal class GlobalNPCFields : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public Player oilPlayer;

    }
}
