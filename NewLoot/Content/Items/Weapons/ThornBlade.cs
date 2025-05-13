using NewLoot.Content.Projectiles;
using Terraria;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Common.Global;

namespace NewLoot.Content.Items.Weapons
{
    public class ThornBlade : ModItem
    {
       
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 5f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 68;
            Item.height = 68;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 70);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

                Item.GetGlobalItem<GlobalFields>().energyCost = 28;
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.UseSound = SoundID.Item113;
                Item.shoot = (ModContent.ProjectileType<ThornRing>());


                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                    {
                        return false;
                    }
                }
                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 26;
                Item.useTime = 26;
                Item.UseSound = SoundID.Item1;
                Item.shoot = (ModContent.ProjectileType<Null>());

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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, velocity, type, damage / 2, knockback/2, player.whoAmI);



            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Vine, 5)
                .AddIngredient(ItemID.Stinger, 4)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
