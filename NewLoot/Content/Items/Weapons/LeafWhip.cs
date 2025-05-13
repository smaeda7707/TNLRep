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

namespace Newloot.Content.Items.Weapons
{
    internal class LeafWhip : ModItem
    {
        
        public override void SetDefaults()
        {
            // Call this method to quickly set some of the properties below.
            //Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 19;
            Item.knockBack = 2.1f;
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<LeafWhipProj>();
            Item.shootSpeed = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.buyPrice(gold: 0, silver: 90);
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
                Item.GetGlobalItem<GlobalFields>().energyCost = 35;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item113;
                Item.shoot = (ModContent.ProjectileType<BranchBall>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 28;
                Item.useTime = 28;
                Item.UseSound = SoundID.Item152;
                Item.shoot = (ModContent.ProjectileType<LeafWhipProj>());

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
    }
}
    

