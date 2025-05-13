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
    internal class PoisonBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true; // Allows right click to be autoswing
        }

        public override void SetDefaults()
        {
            // DefaultToStaff handles setting various Item values that magic staff weapons use.
            // Hover over DefaultToStaff in Visual Studio to read the documentation!
            // Shoot a black bolt, also known as the projectile shot from the onyx blaster.
            Item.DefaultToStaff(ModContent.ProjectileType<PoisonBall>(), 8, 42, 4);
            Item.width = 28;
            Item.height = 30;
            Item.UseSound = SoundID.Item111;
            Item.mana = 9;

            // A special method that sets the damage, knockback, and bonus critical strike chance.
            // This weapon has a crit of 32% which is added to the players default crit chance of 4%
            Item.SetWeaponValues(31, 6, 1); //(Damage,Knockback,CritChance)

            Item.SetShopValues(ItemRarityColor.Blue1, 10000);

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
                Item.GetGlobalItem<GlobalFields>().energyCost = 4;
                Item.mana = 0;
                Item.shoot = ModContent.ProjectileType<VenomBall>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.mana = 9;
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
                .AddIngredient(ModContent.ItemType<DemonitePowder>(), 15)
                .AddIngredient(ItemID.JungleSpores, 6)
                .AddIngredient(ItemID.Leather, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
