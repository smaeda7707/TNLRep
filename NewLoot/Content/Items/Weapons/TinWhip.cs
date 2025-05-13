using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Newloot.Content.Items;
using Newloot.Content.Projectiles;
using NewLoot.Content.Projectiles;
using NewLoot.Common.Players;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;

namespace Newloot.Content.Items.Weapons
{
    internal class TinWhip : ModItem
    {
        
        public override void SetDefaults()
        {
            // Call this method to quickly set some of the properties below.
            //Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 14;
            Item.knockBack = 2.05f;
            Item.rare = ItemRarityID.White;

            Item.shoot = ModContent.ProjectileType<OceanWhipProj>();
            Item.shootSpeed = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.buyPrice(gold: 0, silver: 80);
        }


        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 25;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.UseSound = SoundID.Item113;
                Item.shoot = (ModContent.ProjectileType<Null>());
                Item.useStyle = ItemUseStyleID.RaiseLamp;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 30;
                Item.useTime = 30;
                Item.UseSound = SoundID.Item152;
                Item.shoot = (ModContent.ProjectileType<TinWhipProj>());
                Item.useStyle = ItemUseStyleID.Swing;

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<TinWhipBuff>(), 300);
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }


        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TinBar, 10);
            recipe.AddIngredient(ItemID.Topaz, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
    

