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
    internal class BubbleYoyo : ModItem
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
            Item.width = 30; // The width of the item's hitbox.
            Item.height = 26; // The height of the item's hitbox.

            Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
            Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
            Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
            Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

            Item.damage = 14; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.
            Item.knockBack = 3.65f; // The amount of knockback the item inflicts.
            Item.crit = 0; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
            Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
            Item.rare = ItemRarityID.Blue; // The item's rarity. This changes the color of the item's name.a
            Item.value = Item.buyPrice(gold: 1, silver: 20); // The amount of money that the item is can be bought for.

            Item.shoot = ModContent.ProjectileType<BubbleYoyoProj>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
            Item.shootSpeed = 19.5f; // The velocity of the shot projectile.			
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (!player.GetModPlayer<CoriteYoyoBuffPlayer>().buffed)
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 48;
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
                Item.useAnimation = 25;
                Item.useTime = 25;
                Item.UseSound = SoundID.Item1;
                Item.shoot = (ModContent.ProjectileType<BubbleYoyoProj>());

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

                player.AddBuff(ModContent.BuffType<BubbleExpand>(), 420); 
                
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
