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
    public class OnyxBlade : ModItem
    {
        private int charge;
        private int timer;
        private bool canCharge;
        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.knockBack = 4.8f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.width = 66;
            Item.height = 66;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.shootSpeed = 9.5f;

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 1, 20);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.GetGlobalItem<GlobalFields>().energyCost = 42;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item113;
                Item.noUseGraphic = true;
                Item.useStyle = ItemUseStyleID.RaiseLamp;
                Item.shoot = (ModContent.ProjectileType<Null>());

                var Resource = player.GetModPlayer<Energy>();

                return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
            }
            else
            {
                canCharge = true;
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item1;
                Item.noUseGraphic = false;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useAnimation = 16;
                Item.useTime = 16;

                if (timer > 0)
                {
                    Item.shoot = (ProjectileID.BlackBolt);
                }
                else if (charge >= 3)
                {
                    Item.shoot = (ProjectileID.BlackBolt);
                }
                else
                {
                    Item.shoot = (ModContent.ProjectileType<Null>());
                }

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                timer = 300;
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else if (charge >= 3)
            {
                charge = 0;
            }
            return true;

        }
        public override void UpdateInventory(Player player)
        {
            timer--;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (canCharge)
            {
                charge++;
                canCharge = false;
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = (int) (damage * 1.5f);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 40);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.DarkShard, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
