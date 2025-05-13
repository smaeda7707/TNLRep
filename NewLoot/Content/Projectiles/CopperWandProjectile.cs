using Newloot.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Projectiles;
using Terraria.Audio;
using NewLoot.Content.Items.Weapons;
using System.Runtime.CompilerServices;

namespace Newloot.Content.Projectiles
{
	public class CopperWandProjectile : ModProjectile
	{
		private Vector2 toCursor;
		// Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMin => 22f;
		protected virtual float HoldoutRangeMax => 46f;

		private bool shot;
		public override void SetDefaults() {
			Projectile.width = 30;// Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.height = 30;
			Projectile.aiStyle = ProjAIStyleID.Spear;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
        }

        public override bool PreAI() {
			float flySpeed = 50;
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

			float halfDuration = duration * 0.35f;
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

            if (!shot)
            {
				toCursor = Main.MouseWorld;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 5, ModContent.ProjectileType<AmethystDash>(), (int) (Projectile.damage * 2.25), Projectile.knockBack * 1.1f, Main.myPlayer);
                shot = true;
            }

            Player owner = Main.player[Projectile.owner]; // Get the owner of the projectile.
            Projectile.direction = owner.direction; // Direction will be -1 when facing left and +1 when facing right. 
            owner.heldProj = Projectile.whoAmI; // Set the owner's held projectile to this projectile. heldProj is used so that the projectile will be killed when the player drops or swap items.

            float maxDistance = 11f; // This also sets the maximun speed the projectile can reach while following the cursor.
            Vector2 vectorToCursor = (toCursor - owner.Center).SafeNormalize(Vector2.Zero) * flySpeed;
            float distanceToCursor = 100;
            // Here we can see that the speed of the projectile depends on the distance to the cursor.
            if (distanceToCursor > maxDistance)
            {
                distanceToCursor = maxDistance / distanceToCursor;
                vectorToCursor *= distanceToCursor;
            }

            owner.velocity = vectorToCursor;



            return false; // Don't execute vanilla AI.
		}

    }
}
