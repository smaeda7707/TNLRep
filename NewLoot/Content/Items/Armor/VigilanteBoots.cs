using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Common.Players;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class VigilanteBoots : ModItem
    {
        private int setEnergyCost = 53;
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 15500;
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.moveSpeed -= 0.02f;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 26);
            recipe.AddIngredient(ModContent.ItemType<BoneMarrow>(), 28);
            recipe.AddIngredient(ItemID.Silk, 7);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to enter shadow state for 12 seconds\nWhen in shadow state, the player will not recieve damage\nUses 53 energy";

            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] < 15 && !player.HasBuff(ModContent.BuffType<Shadow>()) && player.GetModPlayer<Energy>().energyCurrent >= setEnergyCost)
            {
                SoundEngine.PlaySound(SoundID.Item103, player.Center);
                player.AddBuff(ModContent.BuffType<Shadow>(), 720);
                player.GetModPlayer<Energy>().energyCurrent -= setEnergyCost;
                player.GetModPlayer<ShadowEyePlayer>().drawEye = true;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<VigilanteCowl>() && body.type == ModContent.ItemType<VigilanteChest>())
            {
                return true;
            }
            return false;
        }
    }

}

public class ShadowEyePlayer : ModPlayer
{
    public bool drawEye;
}


public class ShadowEyeDrawLayer : PlayerDrawLayer
{
    private Asset<Texture2D> exampleItemTexture;
    public int timer = 0;
    public int frame = 1;
    public int timerRate = 3;

    // Returning true in this property makes this layer appear on the minimap player head icon.
    public override bool IsHeadLayer => true;

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        // The layer will be visible only if the player is holding an ExampleItem in their hands. Or if another modder forces this layer to be visible.
        return (drawInfo.drawPlayer.HasBuff<Shadow>() && drawInfo.drawPlayer.GetModPlayer<ShadowEyePlayer>().drawEye);

        // If you'd like to reference another PlayerDrawLayer's visibility,
        // you can do so by getting its instance via ModContent.GetInstance<OtherDrawLayer>(), and calling GetDefaultVisiblity on it
    }

    // This layer will be a 'child' of the head layer, and draw before (beneath) it.
    // If the Head layer is hidden, this layer will also be hidden.
    // If the Head layer is moved, this layer will move with it.
    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Backpacks);
    // If you want to make a layer which isn't a child of another layer, use `new Between(Layer1, Layer2)` to specify the position.
    // If you want to make a 'mobile' layer which can render in different locations depending on the drawInfo, use a `Multiple` position.

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.drawPlayer.GetModPlayer<ShadowEyePlayer>().drawEye)
        {
            timer++;

            // The following code draws ExampleItem's texture behind the player's head.
            if (timer > timerRate)
            {
                if (frame >= 12 || !drawInfo.drawPlayer.HasBuff<Shadow>())
                {
                    // Play explosion sound
                    SoundEngine.PlaySound(SoundID.Item27, drawInfo.drawPlayer.Center);
                    SoundEngine.PlaySound(SoundID.Item131, drawInfo.drawPlayer.Center);
                    // Smoke Dust spawn
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(drawInfo.drawPlayer.TopLeft, 20, 20, DustID.AncientLight, Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1, 1), 100, new Color(102, 92, 194), 2f);
                        dust.noGravity = true;
                        dust.velocity *= 1.8f;
                        dust.velocity.Y *= 1.6f;
                    }

                    // Fire Dust spawn
                    for (int i = 0; i < 40; i++)
                    {
                        Dust dust = Dust.NewDustDirect(drawInfo.drawPlayer.TopLeft, 40, 40, DustID.TintableDust, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), 100, new Color(58, 58, 58), 3f);
                        dust.noGravity = true;
                        dust.velocity *= 3.2f;
                        dust.velocity.Y *= 0.8f;
                        dust.velocity.X *= 1.4f;
                        dust.alpha = 200;
                        dust = Dust.NewDustDirect(drawInfo.drawPlayer.TopLeft, 40, 40, DustID.TintableDust, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), 100, new Color(58, 58, 58), 2f);
                        dust.noGravity = true;
                        dust.alpha = 200;
                        dust.velocity *= 2.8f;
                        dust.velocity.Y *= 0.8f;
                        dust.velocity.X *= 1.2f;
                    }

                    frame = 1;
                    drawInfo.drawPlayer.GetModPlayer<ShadowEyePlayer>().drawEye = false;
                }
                frame++;
                timer = 0;
            }
            if (frame == 1)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye1");
                timerRate = 3;
            }
            else if (frame == 2)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye2");
            }
            else if (frame == 3)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye3");
                timerRate = 15;
            }
            else if (frame == 4)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye4");
                timerRate = 3;
            }
            else if (frame == 5)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye5");
            }
            else if (frame == 6)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye6");
            }
            else if (frame == 7)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye7");
            }
            else if (frame == 8)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye8");
                timerRate = 30;
            }
            else if (frame == 9)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye9");
                timerRate = 15;
            }
            else if (frame == 10)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye10");
            }
            else if (frame == 11)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Content/Items/Armor/ShadowEye11");
            }

            var position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y); // You'll sometimes want to do this, to avoid quivering.

            // Queues a drawing of a sprite. Do not use SpriteBatch in drawlayers!
            drawInfo.DrawDataCache.Add(new DrawData(
                exampleItemTexture.Value, // The texture to render.
                position, // Position to render at.
                null, // Source rectangle.
                new Color(199, 199, 199), // Color.
                0f, // Rotation.
                exampleItemTexture.Size() * 0.5f, // Origin. Uses the texture's center.
                1.4f, // Scale.
                SpriteEffects.None, // SpriteEffects.
                0 // 'Layer'. This is always 0 in Terraria.
            ));
        }
    }
}