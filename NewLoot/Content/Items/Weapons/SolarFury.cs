using NewLoot.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Items.Weapons
{
    // ExampleCustomSwingSword is an example of a sword with a custom swing using a held projectile
    // This is great if you want to make melee weapons with complex swing behaviour
    public class SolarFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.damage = 178;
            Item.knockBack = 5.3f;
            Item.width = 40;
            Item.height = 40;
            Item.scale = 1f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 50); // Sell price is 5 times less than the buy price.
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SolarFuryProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                //Item.shoot = ModContent.ProjectileType<SolarMeteorBig>();

                float PosX = Main.MouseWorld.X; //Makes the projectile always spawn above the cursor
                float PosY = player.position.Y - 600f; //makes the projectile spawn in the sky so it can shoot down
                Projectile.NewProjectile(source, PosX, PosY, 0f, 1f, type, damage, knockback, player.whoAmI);
                return false;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<SolarFuryProjectile>();

                float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
                Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

                return base.Shoot(player, source, position, velocity, type, damage, knockback);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Starfury, 1)
                .AddIngredient(ItemID.MeteoriteBar, 25)
                .AddIngredient(ItemID.FragmentSolar, 15)
                .AddIngredient(ItemID.Ruby, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}