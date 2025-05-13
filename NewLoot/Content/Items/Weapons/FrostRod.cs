using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using NewLoot.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Global;

namespace NewLoot.Content.Items.Weapons
{
    internal class FrostRod : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            // FOR STAFF LIKE BEHAVIOR WHEN SHOOTING
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 62; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 26; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 26; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item25;

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 51; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 3.2f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shootSpeed = 17.25f; // The speed of the projectile (measured in pixels per frame.)
            Item.mana = 10;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 55;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item60;
                Item.mana = 0;
                Item.shoot = ModContent.ProjectileType<Null>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 26;
                Item.useTime = 26;
                Item.UseSound = SoundID.Item28;
                Item.mana = 10;
                Item.shoot = ModContent.ProjectileType<Frostflake>();

                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(ModContent.BuffType<FrostRodBuff>(), 600);
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                return true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<IceWand>())
                .AddIngredient(ItemID.FrostCore)
                .AddIngredient(ItemID.OrichalcumBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}
