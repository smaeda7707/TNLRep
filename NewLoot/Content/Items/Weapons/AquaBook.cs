using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NewLoot.Content.Projectiles;
using Terraria.Enums;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using NewLoot.Common.Global;

namespace NewLoot.Content.Items.Weapons
{
    internal class AquaBook : ModItem
    {
        
        public override void SetDefaults()
        {
            // DefaultToStaff handles setting various Item values that magic staff weapons use.
            // Hover over DefaultToStaff in Visual Studio to read the documentation!
            // Shoot a black bolt, also known as the projectile shot from the onyx blaster.
            Item.DefaultToStaff(ModContent.ProjectileType<AquaRing>(), 5, 36, 4); // (Shoot speed, idk, idk)
            Item.width = 28;
            Item.height = 30;
            Item.UseSound = SoundID.Item21;
            Item.mana = 9;

            // A special method that sets the damage, knockback, and bonus critical strike chance.
            // This weapon has a crit of 32% which is added to the players default crit chance of 4%
            Item.SetWeaponValues(18, 3.1f, 0); //(Damage,Knockback,CritChance)

            Item.SetShopValues(ItemRarityColor.White0, 5000);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 35;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item29;
                Item.mana = 0;
                Item.shoot = ModContent.ProjectileType<Null>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 36;
                Item.useTime = 36;
                Item.UseSound = SoundID.Item21;
                Item.mana = 6;
                Item.shoot = ModContent.ProjectileType<AquaRing>();

                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<AquaBuff>(), 420);

                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }
        }

    }
}
