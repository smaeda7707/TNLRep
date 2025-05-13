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
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace NewLoot.Content.Items.Weapons
{
    public class TechBlade : ModItem
    {
        public static int stage;
        private int count;
        public int resetTimer = 0;

        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.knockBack = 4.75f;
            Item.useStyle = ItemUseStyleID.Swing; // Makes the player do the proper arm motion
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 40;
            Item.height = 48;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.shootSpeed = 6;

            //Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            //Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 3, 60);
            stage = 1;
        }
        public override void UpdateInventory(Player player)
        {
            if (resetTimer++ >= 900) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
                stage = 1;
            if (resetTimer >= 820 && resetTimer % 10 == 0 && stage > 1)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, player.position);
            }
            else if (resetTimer >= 720 && resetTimer < 820 && resetTimer%25 == 0 && stage > 1)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, player.position);
            }
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
                if (stage == 1)
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 10;
                }
                else if (stage == 2)
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 20;
                }
                else if (stage == 3)
                {
                    Item.GetGlobalItem<GlobalFields>().energyCost = 40;
                }
                if (stage < 4)
                {
                    
                    Item.useTime = 35;
                    Item.useAnimation = 35;
                    Item.UseSound = SoundID.Item113;
                    count = 0;
                    resetTimer = 0;
                    var Resource = player.GetModPlayer<Energy>();

                    return Resource.energyCurrent >= Item.GetGlobalItem<GlobalFields>().energyCost;
                }

                else
                {
                    return false;
                }
            }
            else
            {
                if (stage == 1)
                {
                    Item.useAnimation = 20;
                    Item.useTime = 20;
                    Item.scale = 1;
                    Item.shoot = ModContent.ProjectileType<Null>();
                }
                else if (stage == 2)
                {
                    Item.useAnimation = 18;
                    Item.useTime = 18;
                    Item.scale = 1.06f;
                    Item.shoot = ModContent.ProjectileType<Null>();
                }
                else if (stage == 3)
                {
                    Item.shootSpeed = 18;
                    if (count % 2 == 0)
                    {
                        Item.shoot = ModContent.ProjectileType<TechBeamSwordMini>();
                    }
                    else
                    {
                        Item.shoot = ModContent.ProjectileType<Null>();
                    }
                    Item.useAnimation = 16;
                    Item.useTime = 16;
                    Item.scale = 1.13f;
                }
                else
                {
                    Item.shootSpeed = 26;
                    Item.useAnimation = 14;
                    Item.useTime = 14;
                    Item.scale = 1.2f;
                    if (count % 2 == 0)
                    {
                        Item.shoot = ModContent.ProjectileType<TechBeamSword>();
                    }
                    else
                    {
                        Item.shoot = ModContent.ProjectileType<Null>();
                    }

                }
                Item.GetGlobalItem<GlobalFields>().energyCost = 0;
                Item.UseSound = SoundID.Item15;

                return base.CanUseItem(player);
            }


        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                stage++;
                var Resource = player.GetModPlayer<Energy>();

                Resource.energyCurrent -= Item.GetGlobalItem<GlobalFields>().energyCost;
                return true;
            }
            else
            {
                if (stage > 2)
                {
                    count++;
                }
                return true;
            }

        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (stage == 2)
            {
                if (count % 3 == 0)
                {
                    Projectile.NewProjectileDirect(player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<ElectricSpark>(), Item.damage/2, 0, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item75, target.position);
                }
                count++;
            }

        }



    }
    public class TechBladeLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(Terraria.DataStructures.PlayerDrawLayers.HeldItem);
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.type == ModContent.ItemType<TechBlade>() && drawInfo.drawPlayer.ItemAnimationActive;
        }
        private static Asset<Texture2D> texture;
        public override void Load()
        {
            texture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Weapons/TechBladeFrames");
        }
        private void drawSword(ref PlayerDrawSet drawInfo, Color color, int frame)
        {
            Vector2 basePosition = drawInfo.drawPlayer.itemLocation - Main.screenPosition;
            basePosition = new Vector2((int)basePosition.X, (int)basePosition.Y) + (drawInfo.drawPlayer.RotatedRelativePoint(drawInfo.drawPlayer.Center) - drawInfo.drawPlayer.Center);
            Item heldItem = drawInfo.drawPlayer.HeldItem;

            DrawData swingDraw = new DrawData(
            texture.Value, // texture
            basePosition, // position
            new Rectangle(0, texture.Height() / 4 * frame, texture.Width(), texture.Height() / 4), // texture coords
            color, // color (wow really!?)
            drawInfo.drawPlayer.itemRotation,  // rotation
            new Vector2(drawInfo.drawPlayer.direction == -1 ? texture.Value.Width : 0, // origin X
            drawInfo.drawPlayer.gravDir == 1 ? texture.Value.Height / 4 : 0), // origin Y
            drawInfo.drawPlayer.GetAdjustedItemScale(heldItem) * 0 + 1f, // scale (NOT GETTING SCALE BECAUSE DRAW IS DIFFERENT FROM ACTUAl HITBOX) (Get rid of *0 + 1f to get scale)
            drawInfo.itemEffect // sprite effects
            );

            drawInfo.DrawDataCache.Add(swingDraw);
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            int frame;
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.altFunctionUse == 2)
                return;

            if (TechBlade.stage == 1)
            {
                frame = 4;
            }
            else if (TechBlade.stage == 2)
            {
                frame = 1;
            }
            else if (TechBlade.stage == 3)
            {
                frame = 2;
            }
            else
            {
                frame = 3;
            }
            drawSword(ref drawInfo, Color.White, frame);
        }
    }
}
