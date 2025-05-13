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
    internal class ElementalBook : ModItem
    {
        public override void SetDefaults()
        {
            // DefaultToStaff handles setting various Item values that magic staff weapons use.
            // Hover over DefaultToStaff in Visual Studio to read the documentation!
            // Shoot a black bolt, also known as the projectile shot from the onyx blaster.
            Item.DefaultToStaff(ModContent.ProjectileType<Null>(), 8, 26, 11);
            Item.width = 28;
            Item.height = 30;
            Item.UseSound = SoundID.Item111;

            // A special method that sets the damage, knockback, and bonus critical strike chance.
            // This weapon has a crit of 32% which is added to the players default crit chance of 4%
            Item.SetWeaponValues(38, 6, 0); //(Damage,Knockback,CritChance)

            Item.SetShopValues(ItemRarityColor.LightRed4, 10000);

            Item.UseSound = SoundID.Item111;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 64;
                Item.mana = 60;
                Item.shoot = ModContent.ProjectileType<VenomBall>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.mana = 8;
                Item.shoot = ModContent.ProjectileType<PoisonBall>();

                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FrostCore, 1)
                .AddIngredient(ItemID.AncientBattleArmorMaterial, 1)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.JungleSpores, 10)
                .AddIngredient(ItemID.SpellTome)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}
