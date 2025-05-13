using NewLoot.Common.Players;
using NewLoot.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;
using NewLoot.Content.Buffs;
namespace NewLoot.Content.Items.Weapons
{
    internal class DemonYoyo : ModItem
    {

        public override void SetStaticDefaults()
        {
            // These are all related to gamepad controls and don't seem to affect anything else
            ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
            ItemID.Sets.GamepadExtraRange[Item.type] = 15; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
        }

        public override void SetDefaults()
        {
            Item.width = 24; // The width of the item's hitbox.
            Item.height = 24; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 20; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 20; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 19; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 3.35f; // The amount of knockback the item inflicts.
            Item.crit = 0; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
            Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
            Item.rare = ItemRarityID.Green; // The item's rarity. This changes the color of the item's name.a
            Item.value = Item.buyPrice(gold: 3, silver: 50); // The amount of money that the item is can be bought for.

            Item.shootSpeed = 19f; // The velocity of the shot projectile.		
            Item.ArmorPenetration = 2;	
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidian, 20)
                .AddIngredient(ItemID.ShadowScale, 12)
                .AddIngredient(ItemID.WhiteString)
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
                if (!player.HasBuff(ModContent.BuffType<DemonYoyoBuff>()))
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 54;
                    Item.useTime = 5;
                    Item.useAnimation = 5;
                    Item.UseSound = SoundID.Item113;
                    Item.shoot = (ModContent.ProjectileType<Null>());
                }
                

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 20;
                Item.useTime = 20;
                Item.UseSound = SoundID.Item1;
                Item.shoot = (ModContent.ProjectileType<DemonYoyoProj>());

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (!player.HasBuff(ModContent.BuffType<DemonYoyoBuff>()))
                {
                    player.AddBuff(ModContent.BuffType<DemonYoyoBuff>(), 900); 
                }
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
