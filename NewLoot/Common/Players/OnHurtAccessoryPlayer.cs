using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Projectiles;
using NewLoot.Content.Tiles;

namespace NewLoot.Common.Players
{
    internal class OnHurtAccessoryPlayer : ModPlayer
    {
        private int damagePercent;
        private int windChargeEnergyReq = 12;
        private int windChargeDamage;
        public override void PreUpdate()
        {
            damagePercent = Player.statLife / 5;

            if (damagePercent < 20)
            {
                damagePercent = 20;
            }

            windChargeDamage = 3300 / damagePercent;
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            var Resource = Player.GetModPlayer<Energy>();

            if (info.Damage >= damagePercent && Player.GetModPlayer<WindChargePlayer>().equipped && Resource.energyCurrent >= windChargeEnergyReq)
            {
                Vector2 velocity = new Vector2(0, 0);
                Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.position, velocity, ModContent.ProjectileType<WindChargeExplosion>(), windChargeDamage, 10.8f, Player.whoAmI);
                Resource.energyCurrent -= windChargeEnergyReq;
            }
        }
    }
}
