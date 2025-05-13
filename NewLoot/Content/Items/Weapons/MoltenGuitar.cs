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
    internal class MoltenGuitar : ModItem
    {
        
        public int mode;
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 34; // Hitbox width of the item.
            Item.height = 34; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 26; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 26; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item116;

            // Weapon Properties
            Item.DamageType = DamageClass.Melee; // Sets the damage type to ranged.
            Item.damage = 21; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 3.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 12.4f; // The speed of the projectile (measured in pixels per frame.)

            mode = 1;
            Item.mana = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 12;
                Item.useStyle = ItemUseStyleID.RaiseLamp;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item47;
                Item.shoot = ModContent.ProjectileType<Null>();

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.useAnimation = 26;
                Item.useTime = 26;
                Item.useStyle = ItemUseStyleID.Guitar;
                Item.UseSound = SoundID.Item116;
                if (mode == 1)
                {
                    Item.mana = 0;
                    Item.DamageType = DamageClass.Melee;
                    Item.shoot = ModContent.ProjectileType<MoltenArray>();
                }
                else
                {
                    Item.mana = 6;
                    Item.DamageType = DamageClass.Magic;
                    Item.shoot = ModContent.ProjectileType<GuitarRing>();
                }
                return base.CanUseItem(player);
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (mode == 1)
                {
                    mode = 2;
                }
                else
                {
                    mode = 1;
                }
                player.AddBuff(ModContent.BuffType<Exhilaration>(), 360);
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

            if (mode == 1)
            {
                return true; // Return false because we don't want tModLoader to shoot projectile
            }
            else
            {
                Projectile.NewProjectileDirect(source, position = Main.MouseWorld, velocity *= 0, type, (int) (damage * 1.5f), knockback = 0, Main.myPlayer);
                return false; // Return false because we don't want tModLoader to shoot projectile
            }

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 40);
            recipe.AddIngredient(ItemID.PalmWood, 20);
            recipe.AddIngredient(ModContent.ItemType<MoltenString>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
