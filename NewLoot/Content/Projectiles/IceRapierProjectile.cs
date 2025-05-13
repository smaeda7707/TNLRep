using Newloot.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Projectiles;

namespace Newloot.Content.Projectiles
{
	public class IceRapierProjectile : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
		protected virtual float HoldoutRangeMin => 56f;
		protected virtual float HoldoutRangeMax => 76f;

		private int penetrate = 0;

		private bool shot;
		public override void SetDefaults() {
			Projectile.width = 30;// Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.height = 30;
			Projectile.aiStyle = ProjAIStyleID.Spear;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 18;
        }

        public override bool PreAI() {
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

			if (Projectile.ai[1] == 100)
			{
                if (!shot)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 10, ModContent.ProjectileType<SnowflakeShard>(), (int) (Projectile.damage * 0.7), Projectile.knockBack/2, Main.myPlayer);
					shot = true;
                }
            }


            return false; // Don't execute vanilla AI.
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // The following code is used to limit how many times the projectile can hit


            int penetrateMax = 2; // How many times the projectile should be able to hit (Don't go below 2)

            if (penetrate < penetrateMax - 2)
            {
                penetrate++;
            }
            else
            {
                Projectile.localNPCHitCooldown = -1;
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 1.09f;
        }

    }
}
