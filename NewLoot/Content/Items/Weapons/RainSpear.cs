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
using NewLoot.Common.Global;

namespace Newloot.Content.Items.Weapons
{
    internal class RainSpear : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {

            // Use and Animation Style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<RainSpearProjectile>();
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 6.5f;
            

            // Damage Values
            Item.DamageType = DamageClass.Melee;
            Item.damage = 13;
            Item.knockBack = 6.5f;
            Item.crit = 0;

            // Rarity and Price
            Item.value = Item.buyPrice(silver: 70);
            Item.rare = ItemRarityID.White;

            
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.GetGlobalItem<GlobalFields>().energyCost = 12;
                Item.useTime = 25;
                Item.useAnimation = 25;
                Item.shoot = (ModContent.ProjectileType<RainSpearThrown>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 33;
                Item.useTime = 33;
                Item.shoot = (ModContent.ProjectileType<RainSpearProjectile>());

                // Ensures no more than one spear can be thrown out, use this when using autoReuse
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
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
                if (!Main.dedServ && Item.UseSound.HasValue)
                {
                    SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
                }
                return true;
            }


        }

    }
  
}

