using Newloot.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Projectiles;
using System;
using NewLoot.Content.Buffs;
using System.Reflection;

namespace Newloot.Content.Projectiles
{
	public class PowerSpearProjectileSecondary : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMin => 40f;
		protected virtual float HoldoutRangeMax => 145f;


		public override void SetDefaults() {
			Projectile.width = 30;// Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.height = 30;
			Projectile.aiStyle = ProjAIStyleID.Spear;
			Projectile.friendly = false;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.scale = 1.2f;
        }

        public override bool PreAI() {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;
            
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

			float halfDuration = duration * 0.5f;
			float progress;

			// Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			// Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

			// Apply proper rotation to the sprite.
			if (Projectile.spriteDirection == -1) {
				// If sprite is facing left, rotate 45 degrees
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else {
				// If sprite is facing right, rotate 135 degrees
				Projectile.rotation += MathHelper.ToRadians(135f);
			}
			SimpleCustomCollisionLogic(Projectile.getRect(), 2, player);

            return false; // Don't execute vanilla AI.
		}
        public void SimpleCustomCollisionLogic(Rectangle projRect, int allowed, Player player)
        {
            //var type = member.Key;
            //var time = member.Value.Time;

            //allowed = 1 for only players, 2 for only npc's, 0 for both
            if (allowed != 1)
            {
                int index = Array.FindIndex(Main.npc, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
					player.AddBuff(ModContent.BuffType<PowerSpearBuff>(), 90);
                }
            }
            if (allowed != 2)
            {
                int index = Array.FindIndex(Main.player, target => Projectile.Colliding(projRect, target.getRect()) && target.active);
                if (index != -1)
                {
                    Main.player[index].AddBuff(ModContent.BuffType<CorruptSandstormBuff>(), 1);
                    //dict.Remove(type);
                }
            }
        }
    }
}
