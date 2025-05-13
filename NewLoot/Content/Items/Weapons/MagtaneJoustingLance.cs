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
using Terraria.Enums;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Weapons
{
    public class MagtaneJoustingLance : ModItem
    {
        
        public override void SetDefaults()
        {
            // A special method that sets a variety of item parameters that make the item act like a spear weapon.
            // To see everything DefaultToSpear() does, right click the method in Visual Studios and choose "Go To Definition" (or press F12). You can also hover over DefaultToSpear to see the documentation.
            // The shoot speed will affect how far away the projectile spawns from the player's hand.
            // If you are using the custom AI in your projectile (and not aiStyle 19 and AIType = ProjectileID.JoustingLance), the standard value is 1f.
            // If you are using aiStyle 19 and AIType = ProjectileID.JoustingLance, then multiply the value by about 3.5f.
            Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.MagtaneJoustingLanceProjectile>(), 1f, 24);

            Item.DamageType = DamageClass.MeleeNoSpeed; // We need to use MeleeNoSpeed here so that attack speed doesn't effect our held projectile.

            Item.SetWeaponValues(17, 7.5f, 0); // A special method that sets the damage, knockback, and bonus critical strike chance.

            Item.SetShopValues(ItemRarityColor.White0, Item.buyPrice(0,0, 60)); // A special method that sets the rarity and value.

            Item.channel = true; // Channel is important for our projectile.

            // This will make sure our projectile completely disappears on hurt.
            // It's not enough just to stop the channel, as the lance can still deal damage while being stowed
            // If two players charge at each other, the first one to hit should cancel the other's lance
            Item.StopAnimationOnHurt = true;
        }

        // This will allow our Jousting Lance to receive the same modifiers as melee weapons.
        public override bool MeleePrefix()
        {
            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MagtaneBar>(12)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.Glass, 40)
                .AddTile(TileID.Anvils)
                .Register();
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
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item113;
                Item.useStyle = ItemUseStyleID.RaiseLamp;
                Item.shoot = (ModContent.ProjectileType<Null>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 24;
                Item.useTime = 24;
                Item.UseSound = SoundID.Item1;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = (ModContent.ProjectileType<MagtaneJoustingLanceProjectile>());

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<MagnetSpeed>(), 180);
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
    }
}
