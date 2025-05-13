using NewLoot.Content.Projectiles;
using Terraria;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using Newloot.Content.Projectiles;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Weapons
{
    public class OilBlade : ModItem
    {
        private int count;
        public override void SetDefaults()
        {
            Item.damage = 87;
            Item.knockBack = 4.75f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.width = 54;
            Item.height = 70;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.shootSpeed = 9;
            Item.noUseGraphic = false;

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 6, 80);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 30;
                Item.useTime = 16;
                Item.useAnimation = 16;
                Item.UseSound = SoundID.Item35;
                Item.useStyle = ItemUseStyleID.Guitar;
                Item.shoot = (ModContent.ProjectileType<Null>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Swing;

                if (count % 4 == 0)
                {
                    Item.shoot = (ModContent.ProjectileType<OilDrop>());
                }
                else
                {
                    Item.shoot = (ModContent.ProjectileType<Null>());
                }

                Item.useTime = 28;
                Item.useAnimation = 28;
                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<BayBladeBuff>(), 540);
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                count++;
                return true;
            }


        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity * 2, ModContent.ProjectileType<FlammableSpark>(), damage, knockback/4, player.whoAmI);
            }
            else
            {
                GlobalItemMethods.CreateRotatedProjectiles(player, source, position, velocity, type, damage / 3, 10, 3, knockback / 3, true);
            }
            return false;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    }
}
