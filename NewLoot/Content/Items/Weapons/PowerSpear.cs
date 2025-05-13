using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Newloot.Content.Projectiles;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using NewLoot.Content.Items;
using NewLoot.Common.Global;

namespace Newloot.Content.Items.Weapons
{
    internal class PowerSpear : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {

            // Use and Animation Style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PowerSpearProjectile>();
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item71;
            Item.shootSpeed = 6;
            

            // Damage Values
            Item.DamageType = DamageClass.Melee;
            Item.damage = 52;
            Item.knockBack = 6.6f;
            Item.crit = 0;

            // Rarity and Price
            Item.value = Item.buyPrice(gold: 4, silver: 10);
            Item.rare = ItemRarityID.Lime;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 10;
                Item.useTime = 80;
                Item.useAnimation = 80;
                Item.shoot = (ModContent.ProjectileType<PowerSpearProjectileSecondary>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 2;
                Item.useAnimation = 28;
                Item.useTime = 28;
                Item.shoot = (ModContent.ProjectileType<PowerSpearProjectile>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;

            }
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 1;

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {

            var Resource = player.GetModPlayer<Energy>();

            Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;

            return true;


        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); // Creates Recipe
            recipe.AddIngredient(ModContent.ItemType<CoriteBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<PureChlorophite>(), 12);
            recipe.AddIngredient(ItemID.Wire, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
  
}

