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
    public class BayBlade : ModItem
    {
        public static bool waveMode;
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.knockBack = 4.75f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.width = 40;
            Item.height = 48;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.shootSpeed = 9;
            waveMode = false;

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.Blue;
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
                Item.GetGlobalItem<GlobalFields>().energyCost = 30;
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
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item1;
                Item.noUseGraphic = false;
                Item.useStyle = ItemUseStyleID.Swing;

                if (waveMode)
                {
                    Item.shoot = (ModContent.ProjectileType<Wave>());
                }
                else
                {
                    Item.shoot = (ModContent.ProjectileType<Null>());
                }

                if (player.HasBuff(ModContent.BuffType<BayBladeBuff>()))
                {
                    Item.useAnimation = 20;
                    Item.useTime = 20;
                }
                else
                {
                    Item.useAnimation = 24;
                    Item.useTime = 24;
                }
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
                waveMode = false;
                return true;
            }


        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            waveMode = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage /= 2;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    }
}
