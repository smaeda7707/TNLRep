using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Items.Armor;
using NewLoot.Common.Players;
using System;
using NewLoot.Content.Buffs;
using System.Threading;
using Terraria.Audio;
using NewLoot.Content.Items.Accessories;
using Mono.Cecil;
using NewLoot.Content.Projectiles;
using Terraria.DataStructures;

namespace NewLoot.Common.Global
{
    internal class GlobalItemMethods : GlobalItem
    {
        public static int CreateRotatedProjectilesWithGunLoad(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float rotateNum, int gunLoad, int numProjectiles, float knockback, bool decreaseVelocityOfLastTwo)
        {
            float rotateNumUse = 0;

            for (int f = 0; f < (numProjectiles/2); f++)
            {
                rotateNumUse -= rotateNum;
            }
            if (numProjectiles%2 == 0) // Offset the starting position for even amount of projectiles so that they are equaly distant from the cursor
            {
                rotateNumUse += rotateNum / 2;
            }
            for (int i = 0; i < numProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNumUse));

                if (decreaseVelocityOfLastTwo && (i == 0 || i == numProjectiles - 1))
                {
                    newVelocity *= 0.95f;
                }

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                rotateNumUse += rotateNum;
                gunLoad -= 1;
            }
            return gunLoad;
        }

        public static void CreateRotatedProjectiles(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, int rotateNum, int numProjectiles, float knockback, bool decreaseVelocityOfLastTwo)
        {
            float rotateNumUse = 0;

            for (int f = 0; f < (numProjectiles / 2); f++)
            {
                rotateNumUse -= rotateNum;
            }
            if (numProjectiles % 2 == 0) // Offset the starting position for even amount of projectiles so that they are equaly distant from the cursor
            {
                rotateNumUse += rotateNum/2;
            }
            for (int i = 0; i < numProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNumUse));

                if (decreaseVelocityOfLastTwo && (i == 0 || i == numProjectiles -1))
                {
                    newVelocity *= 0.9f;
                }

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                rotateNumUse += rotateNum;
            }
        }
    }
    internal class GlobalProjectileMethods : GlobalProjectile
    {
        public static void CreateRotatedProjectiles(Player player, IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, int rotateNum, int numProjectiles, float knockback, bool decreaseVelocityOfLastTwo)
        {
            float rotateNumUse = 0;

            for (int f = 0; f < (numProjectiles / 2); f++)
            {
                rotateNumUse -= rotateNum;
            }
            if (numProjectiles % 2 == 0) // Offset the starting position for even amount of projectiles so that they are equaly distant from the cursor
            {
                rotateNumUse += rotateNum / 2;
            }
            for (int i = 0; i < numProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(rotateNumUse));

                if (decreaseVelocityOfLastTwo && (i == 0 || i == numProjectiles - 1))
                {
                    newVelocity *= 0.9f;
                }

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                rotateNumUse += rotateNum;
            }
        }
    }
}
