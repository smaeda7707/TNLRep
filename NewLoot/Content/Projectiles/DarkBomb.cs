using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewLoot.Content.Buffs;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewLoot.Content.Projectiles
{
    public class DarkBomb : ModProjectile
    {
        private const int DefaultWidthHeight = 15;
        private const int ExplosionWidthHeight = 125;
        private bool hitCheck;
        private int timer;
        private bool portalMultiCheck;
        public static bool charging;
        public static float charge;
        public override void SetDefaults()
        {
            // While the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            Projectile.width = DefaultWidthHeight;
            Projectile.height = DefaultWidthHeight;
            Projectile.friendly = false;
            Projectile.penetrate = -1;

            // 5 second fuse.
            Projectile.timeLeft = 180;

            // These help the projectile hitbox be centered on the projectile sprite.
            DrawOffsetX = -2;
            DrawOriginOffsetY = -5;
        }
        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float ChargeTime
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        private bool Charge(Player owner)
        {
            // Like other whips, this whip updates twice per frame (Projectile.extraUpdates = 1), so 120 is equal to 1 second.
            if (!owner.channel || ChargeTime >= 210)
            {
                return true; // finished charging
            }

            ChargeTime++;

            if (ChargeTime % 60 == 0) // 1 segment per 12 ticks of charge.
                Projectile.damage = (int) (Projectile.damage * 1.5);

            charge = ChargeTime;
            // Reset the animation and item timer while charging.
            owner.itemAnimation = owner.itemAnimationMax;
            owner.itemTime = owner.itemTimeMax;

            return false; // still charging
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    modifiers.FinalDamage /= 5;
                }
            }
        }
        public void SimpleCustomCollisionLogic(Rectangle projRect, int allowed)
        {
            //var type = member.Key;
            //var time = member.Value.Time;

            //allowed = 1 for only players, 2 for only npc's, 0 for both
            if (allowed != 1)
            {
                int index = Array.FindIndex(Main.npc, target => Projectile.Colliding(projRect, target.getRect()) && target.active && hitCheck == false);
                if (index != -1)
                {
                    hitCheck = true;
                }
            }
        }

        // The projectile is very bouncy, but the spawned children projectiles shouldn't bounce at all.
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Die immediately if ai[1] isn't 0 (We set this to 1 for the 5 extra explosives we spawn in Kill)
            if (Projectile.ai[1] != 0)
            {
                return true;
            }

            // This code makes the projectile very bouncy.
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.9f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.9f;
            }
            return false;
        }

        public override void AI()
        {
            // remove these 3 lines if you don't want the charging mechanic
            if (!Charge(Main.player[Projectile.owner]))
            {
                if (!portalMultiCheck)
                {
                    charging = true;
                    Projectile.timeLeft = 180;
                    Projectile.alpha = 255;
                    Projectile.velocity *= 0;
                    Projectile.position = Main.MouseWorld;
                    return; // timer doesn't update while charging, freezing the animation at the start.
                }
                else
                {
                    charging = false;
                }
            }
            else
            {
                charging = false;
                portalMultiCheck = true;
                SimpleCustomCollisionLogic(Projectile.getRect(), 2);
                // The projectile is in the midst of exploding during the last 3 updates.
                if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3 || hitCheck)
                {
                    Projectile.friendly = true;
                    Projectile.tileCollide = false;
                    // Set to transparent. This projectile technically lives as transparent for about 3 frames
                    Projectile.alpha = 255;

                    // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                    Projectile.Resize(ExplosionWidthHeight, ExplosionWidthHeight);

                    if (timer++ >= 3)
                    {
                        Projectile.Kill();
                    }
                }
                else
                {
                    // Smoke and fuse dust spawn. The position is calculated to spawn the dust directly on the fuse.
                    if (Main.rand.NextBool())
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                        dust.scale = 0.1f + Main.rand.Next(5) * 0.1f;
                        dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                        dust.noGravity = true;
                        dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - 2.1f, default) * 10f;

                        dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, 0f, 0f, 100, default, 1f);
                        dust.scale = 1f + Main.rand.Next(5) * 0.1f;
                        dust.noGravity = true;
                        dust.position = Projectile.Center + new Vector2(1, 0).RotatedBy(Projectile.rotation - 2.1f, default) * 10f;
                    }
                    Projectile.alpha = 0;
                }
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] > 10f)
                {
                    Projectile.ai[0] = 10f;
                    // Roll speed dampening. 
                    if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.96f;

                        if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                        {
                            Projectile.velocity.X = 0f;
                            Projectile.netUpdate = true;
                        }
                    }
                    // Delayed gravity
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                }
                // Rotation increased by velocity.X 
                Projectile.rotation += Projectile.velocity.X * 0.1f;
            }

            Timer++;
            
        }

        public override void OnKill(int timeLeft)
        {

            // Play explosion sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 25; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                dust.velocity *= 0.7f;
            }

            // Fire Dust spawn
            for (int i = 0; i < 40; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, 0f, 0f, 100, default, 3f);
                dust.noGravity = true;
                dust.velocity *= 2.5f;
                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, 0f, 0f, 100, default, 2f);
                dust.velocity *= 1.5f;
                dust.noGravity = true;
            }

            // Large Smoke Gore spawn
            for (int g = 0; g < 2; g++)
            {
                var goreSpawnPosition = new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f);
                Gore gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y += 1.5f;
                gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X += 1.5f;
                gore.velocity.Y -= 1.5f;
                gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
                gore.scale = 1.5f;
                gore.velocity.X -= 1.5f;
                gore.velocity.Y -= 1.5f;
            }
            // reset size to normal width and height.
            Projectile.Resize(DefaultWidthHeight, DefaultWidthHeight);

           
        }
    }
    public class PortalDraw : PlayerDrawLayer
    {
        private Asset<Texture2D> exampleItemTexture;
        public int timer = 0;

        // Returning true in this property makes this layer appear on the minimap player head icon.
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // The layer will be visible only if the player is holding an ExampleItem in their hands. Or if another modder forces this layer to be visible.
            return DarkBomb.charging;

            // If you'd like to reference another PlayerDrawLayer's visibility,
            // you can do so by getting its instance via ModContent.GetInstance<OtherDrawLayer>(), and calling GetDefaultVisiblity on it
        }

        // This layer will be a 'child' of the head layer, and draw before (beneath) it.
        // If the Head layer is hidden, this layer will also be hidden.
        // If the Head layer is moved, this layer will move with it.
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.FaceAcc);
        // If you want to make a layer which isn't a child of another layer, use `new Between(Layer1, Layer2)` to specify the position.
        // If you want to make a 'mobile' layer which can render in different locations depending on the drawInfo, use a `Multiple` position.

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            timer ++;
            if (timer >= 45)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Projectiles/PortalFourthFrame");
            }
            else if (timer >= 30)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Projectiles/PortalThirdFrame");
            }
            else if (timer >= 15)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Projectiles/PortalSecondFrame");
            }
            else
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Projectiles/PortalFirstFrame");
            }
            if (timer == 60)
            {
                timer = 0;
            }


            var position = Main.MouseScreen;
            position = new Vector2((int)position.X, (int)position.Y); // You'll sometimes want to do this, to avoid quivering.

            // Queues a drawing of a sprite. Do not use SpriteBatch in drawlayers!
            drawInfo.DrawDataCache.Add(new DrawData(
                exampleItemTexture.Value, // The texture to render.
                position, // Position to render at.
                null, // Source rectangle.
                Color.Purple, // Color.
                0f, // Rotation.
                exampleItemTexture.Size() * 0.5f, // Origin. Uses the texture's center.
                1f, // Scale.
                SpriteEffects.None, // SpriteEffects.
                0 // 'Layer'. This is always 0 in Terraria.
            ));
        }

    }
}
