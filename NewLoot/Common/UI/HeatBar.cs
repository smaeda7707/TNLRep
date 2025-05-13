using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using NewLoot.Common.Players;
using NewLoot.Content.Items.Weapons;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.Localization;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Buffs;
using Humanizer;

namespace NewLoot.Common.UI.energyUI
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in energyPlayer
    internal class HeatBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 1340, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(620, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("NewLoot/Common/UI/HeatBarFrame")); // Frame of our resource bar
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(74, 0f);
            barFrame.Height.Set(20, 0f);

            text = new UIText("0/0", 0.6f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(8, 0f);
            text.Left.Set(-10, 0f);

            gradientA = new Color(98, 12, 2); // A dark purple
            gradientB = new Color(255, 78, 47); // A light purple

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (Main.LocalPlayer.HeldItem.ModItem is not MagtaneRepeater && Main.LocalPlayer.HeldItem.ModItem is not RageRepeater && Main.LocalPlayer.HeldItem.ModItem is not MoltenShotbow)
                return;

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float quotient;
            base.DrawSelf(spriteBatch);

            // Calculate quotient
            if (Main.LocalPlayer.GetModPlayer<OverheatPlayer>().overheated == true)
            {
                quotient = 1;
            }
            else
            {
                if (Main.LocalPlayer.HeldItem.ModItem is MagtaneRepeater)
                {
                    quotient = (float)(MagtaneRepeater.magtaneRepeaterShots % MagtaneRepeater.repeaterMaxShots) / MagtaneRepeater.repeaterMaxShots; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
                }
                else if (Main.LocalPlayer.HeldItem.ModItem is RageRepeater)
                {
                    quotient = (float)(RageRepeater.rageRepeaterShots % RageRepeater.repeaterMaxShots) / RageRepeater.repeaterMaxShots; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
                }
                else if (Main.LocalPlayer.HeldItem.ModItem is MoltenShotbow)
                {
                    quotient = (float)(MoltenShotbow.repeaterShots % MoltenShotbow.repeaterMaxShots) / MoltenShotbow.repeaterMaxShots; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
                }
                else
                {
                    quotient = 1;
                }
                    
            }
                
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 7;
            hitbox.Width -= 12;
            hitbox.Y += 7;
            hitbox.Height -= 10;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Setting the text per tick to update and show our resource values.
            text.SetText(HeatUISystem.HeatText.Format(MagtaneRepeater.magtaneRepeaterShots, MagtaneRepeater.repeaterMaxShots));
            base.Update(gameTime);
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class HeatUISystem : ModSystem
    {
        private UserInterface heatBarUserInterface;

        internal HeatBar heatBar;

        public static LocalizedText HeatText { get; private set; }

        public override void Load()
        {
            heatBar = new();
            heatBarUserInterface = new();
            heatBarUserInterface.SetState(heatBar);

            string category = "UI";
            HeatText ??= Language.GetOrRegister(Mod.GetLocalizationKey($"{category}.Heat"));
        }

        public override void UpdateUI(GameTime gameTime)
        {
            heatBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "NewLoot: Heat Bar",
                    delegate {
                        heatBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
