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
    public class DustGun : ModItem
    {
        
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 28; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 28; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item30;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 16; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 2.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<SandGust>(); // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 0; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 2;

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
                Item.shoot = (ModContent.ProjectileType<SandGust>());

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
