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

namespace NewLoot.Content.Items.Weapons
{
    public class DemonicScimitar : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.knockBack = 4.75f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.width = 40;
            Item.height = 48;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.shootSpeed = 6;

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1, 75);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 65;
                    Item.useTime = 35;
                    Item.useAnimation = 35;
                    Item.UseSound = SoundID.Item113;
                    Item.shoot = (ModContent.ProjectileType<CorruptSandstormEye>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 14;
                Item.useTime = 14;
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
                Item.scale = 1.25f;
                return true;
            }
            else
            {
                Item.scale = 1.25f;
                return true;
            }

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AncientScimitar>())
                .AddIngredient(ItemID.DemoniteBar, 10)
                .AddIngredient(ItemID.ShadowScale, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
