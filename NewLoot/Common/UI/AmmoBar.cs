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
using System.Runtime.CompilerServices;
using System.Linq;

namespace NewLoot.Common.UI.energyUI
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in energyPlayer
    internal class AmmoBar : UIState
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

            barFrame = new UIImage(ModContent.Request<Texture2D>("NewLoot/Common/UI/AmmoBarFrame")); // Frame of our resource bar
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(74, 0f);
            barFrame.Height.Set(20, 0f);

            text = new UIText("0/0", 0.6f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(8, 0f);
            text.Left.Set(-10, 0f);

            gradientA = new Color(255, 231, 97); // A Light yellow
            gradientB = new Color(255, 216, 0); // A Dark yellow


            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (SharkGun.gunLoad == SharkGun.maxLoad && Main.LocalPlayer.HeldItem.ModItem is SharkGun)
            {
                gradientA = new Color(155, 21, 94);
                gradientB = new Color(81, 223, 241);
            }
            else if (PoisonKnives.gunLoad == PoisonKnives.maxLoad && Main.LocalPlayer.HeldItem.ModItem is PoisonKnives)
            {
                gradientA = new Color(72, 217, 93);
                gradientB = new Color(255, 216, 0);
            }
            else if (CoriteCannon.gunLoad == CoriteCannon.maxLoad && Main.LocalPlayer.HeldItem.ModItem is CoriteCannon || WindCannon.gunLoad == WindCannon.maxLoad && Main.LocalPlayer.HeldItem.ModItem is WindCannon)
            {
                gradientA = new Color(163, 163, 163);
                gradientB = new Color(255, 255, 255);
            }
            else if (BladeShotgun.gunLoad == BladeShotgun.maxLoad && Main.LocalPlayer.HeldItem.ModItem is BladeShotgun)
            {
                gradientA = new Color(255, 231, 97);
                gradientB = new Color(53, 97, 197);
            }
            else if (SlimeLauncher.gunLoad == SlimeLauncher.maxLoad && Main.LocalPlayer.HeldItem.ModItem is SlimeLauncher)
            {
                gradientA = new Color(86, 152, 255);
                gradientB = new Color(43, 60, 95);
            }
            else
            {
                gradientA = new Color(255, 231, 97); // A Light yellow
                gradientB = new Color(255, 216, 0); // A Dark yellow
            }
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (Main.LocalPlayer.HeldItem.ModItem is not CoriteBlaster && Main.LocalPlayer.HeldItem.ModItem is not BambooGun && Main.LocalPlayer.HeldItem.ModItem is not SharkGun && Main.LocalPlayer.HeldItem.ModItem is not Blunderbuss && Main.LocalPlayer.HeldItem.ModItem is not PoisonKnives && Main.LocalPlayer.HeldItem.ModItem is not CoriteCannon && Main.LocalPlayer.HeldItem.ModItem is not BladeShotgun && Main.LocalPlayer.HeldItem.ModItem is not WindCannon && Main.LocalPlayer.HeldItem.ModItem is not SlimeLauncher)
                return;

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //int heldItem = Main.LocalPlayer.HeldItem.type;
            //int[] noEmpower = { ModContent.ItemType<CoriteBlaster>(), ModContent.ItemType<BambooGun>(), ModContent.ItemType<Blunderbuss>() };
            float quotient;
            base.DrawSelf(spriteBatch);

            //if (noEmpower.Contains(heldItem))
            //{
                //quotient = (float) / heldItem.maxLoad;
            //}

            if (Main.LocalPlayer.HeldItem.ModItem is CoriteBlaster)
            {
                quotient = (float)CoriteBlaster.gunLoad / CoriteBlaster.maxLoad; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is BambooGun)
            {
                quotient = (float) BambooGun.gunLoad / BambooGun.maxLoad; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is SharkGun)
            {
                quotient = (float)SharkGun.gunLoad / (SharkGun.maxLoad -1); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is PoisonKnives)
            {
                quotient = (float)PoisonKnives.gunLoad / (PoisonKnives.maxLoad - 3); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is Blunderbuss)
            {
                quotient = (float)Blunderbuss.gunLoad / Blunderbuss.maxLoad; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is CoriteCannon)
            {
                quotient = (float)CoriteCannon.gunLoad / (CoriteCannon.maxLoad-1); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is BladeShotgun)
            {
                quotient = (float)BladeShotgun.gunLoad / (BladeShotgun.maxLoad - 4); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is WindCannon)
            {
                quotient = (float)WindCannon.gunLoad / (WindCannon.maxLoad - 1); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else if (Main.LocalPlayer.HeldItem.ModItem is SlimeLauncher)
            {
                quotient = (float)SlimeLauncher.gunLoad / (SlimeLauncher.maxLoad - 1); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            }
            else
            {
                quotient = 1;
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
            text.SetText(AmmoUISystem.AmmoText.Format(MagtaneRepeater.magtaneRepeaterShots, MagtaneRepeater.repeaterMaxShots));
            base.Update(gameTime);
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class AmmoUISystem : ModSystem
    {
        private UserInterface ammoBarUserInterface;

        internal AmmoBar ammoBar;

        public static LocalizedText AmmoText { get; private set; }

        public override void Load()
        {
            ammoBar = new();
            ammoBarUserInterface = new();
            ammoBarUserInterface.SetState(ammoBar);

            string category = "UI";
            AmmoText ??= Language.GetOrRegister(Mod.GetLocalizationKey($"{category}.Ammo"));
        }

        public override void UpdateUI(GameTime gameTime)
        {
            ammoBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "NewLoot: Ammo Bar",
                    delegate {
                        ammoBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
