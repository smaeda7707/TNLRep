﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using NewLoot.Content.Buffs;
using Terraria.ID;

namespace NewLoot.Common
{
    public class ExamplePlayerDrawLayer : PlayerDrawLayer
    {
        private Asset<Texture2D> exampleItemTexture;
        public int timer = 0;
        int frame = 1;

        // Returning true in this property makes this layer appear on the minimap player head icon.
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // The layer will be visible only if the player is holding an ExampleItem in their hands. Or if another modder forces this layer to be visible.
            return (drawInfo.drawPlayer.HasBuff<SubZeroBuff>() || drawInfo.drawPlayer.HasBuff<HeatWaveBuff>());

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
            timer ++;
            // The following code draws ExampleItem's texture behind the player's head.
            if (timer > 3)
            {
                if (frame >= 4)
                {
                    frame = 0;
                }
                frame++;
                timer = 0;
            }
            if (frame == 1)
            {
               exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Common/UltimateAura2FirstFrame");
            }
            else if (frame == 2)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Common/UltimateAura2SecondFrame");
            }
            else if (frame == 3)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Common/UltimateAura2ThirdFrame");
            }
            else if (frame == 4)
            {
                exampleItemTexture = ModContent.Request<Texture2D>("NewLoot/Common/UltimateAura2LastFrame");
            }

            var position = drawInfo.Center + new Vector2(0f, -20f) - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y); // You'll sometimes want to do this, to avoid quivering.

            // Queues a drawing of a sprite. Do not use SpriteBatch in drawlayers!
            drawInfo.DrawDataCache.Add(new DrawData(
                exampleItemTexture.Value, // The texture to render.
                position, // Position to render at.
                null, // Source rectangle.
                Color.DarkBlue, // Color.
                0f, // Rotation.
                exampleItemTexture.Size() * 0.5f, // Origin. Uses the texture's center.
                1f, // Scale.
                SpriteEffects.None, // SpriteEffects.
                0 // 'Layer'. This is always 0 in Terraria.
            ));
        }

    }
}
