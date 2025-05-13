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
    internal class DarkGlaive : ModItem
    {
        public int attackType = 0; // keeps track of which attack it is
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {

            // Use and Animation Style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 10.2f;
            Item.shoot = (ModContent.ProjectileType<DarkGlaiveProjectile>());


            // Damage Values
            Item.DamageType = DamageClass.Melee;
            Item.damage = 14;
            Item.knockBack = 6.5f;
            Item.crit = 2;

            // Rarity and Price
            Item.value = Item.buyPrice(silver: 90);
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 19;
                Item.useTime = 20;
                Item.useAnimation = 20;

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 28;
                Item.useTime = 28;

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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DarkGlaiveProjSecondary>(), damage * 3, knockback, Main.myPlayer, attackType);
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DarkGlaiveProjectile>(), damage, knockback, Main.myPlayer, ProjAIStyleID.Spear);
            }
            
            return false; // return false to prevent original projectile from being shot
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 12)
                .AddIngredient(ItemID.Amethyst, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
  
}

