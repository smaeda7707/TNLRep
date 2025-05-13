using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewLoot.Common.Players
{
    public class Energy : ModPlayer
    {
        // Here we create a custom resource, similar to mana or health.
        // Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
        public int energyCurrent; // Current value of our example resource
        public const int DefaultEnergyResourceMax = 80; // Default maximum value of example resource
        public int energyMax; // Buffer variable that is used to reset maximum resource to default value in ResetDefaults().
        public int energyMax2; // Maximum amount of our example resource. We will change that variable to increase maximum amount of our resource
        public float energyRegenRate; // By changing that variable we can increase/decrease regeneration rate of our resource
        internal int energyRegenTimer = 0; // A variable that is required for our timer
        public static readonly Color HealEnergyResource = new(52, 235, 73); // We can use this for CombatText, if you create an item that replenishes energyCurrent.

        // In order to make the Example Resource example straightforward, several things have been left out that would be needed for a fully functional resource similar to mana and health. 
        // Here are additional things you might need to implement if you intend to make a custom resource:
        // - Multiplayer Syncing: The current example doesn't require MP code, but pretty much any additional functionality will require this. ModPlayer.SendClientChanges and CopyClientState will be necessary, as well as SyncPlayer if you allow the user to increase energyMax.
        // - Save/Load permanent changes to max resource: You'll need to implement Save/Load to remember increases to your energyMax cap.
        // - Resouce replenishment item: Use GlobalNPC.OnKill to drop the item. ModItem.OnPickup and ModItem.ItemSpace will allow it to behave like Mana Star or Heart. Use code similar to Player.HealEffect to spawn (and sync) a colored number suitable to your resource.

        public override void Initialize()
        {
            energyMax = DefaultEnergyResourceMax;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        // We need this to ensure that regeneration rate and maximum amount are reset to default values after increasing when conditions are no longer satisfied (e.g. we unequip an accessory that increaces our recource)
        private void ResetVariables()
        {
            energyRegenRate = 1f;
            energyMax2 = energyMax;
        }

        public override void PostUpdateMiscEffects()
        {
            UpdateResource();
        }

        public override void PostUpdate()
        {
            CapResourceGodMode();
        }

        // Lets do all our logic for the custom resource here, such as limiting it, increasing it and so on.
        private void UpdateResource()
        {
            // For our resource lets make it regen slowly over time to keep it simple, let's use energyRegenTimer to count up to whatever value we want, then increase currentResource.
            energyRegenTimer++; // Increase it by 60 per second, or 1 per tick.

            // A simple timer that goes up to 1 second, increases the energyCurrent by 1 and then resets back to 0.
            if (energyRegenTimer > 60 / energyRegenRate)
            {
                energyCurrent += 1;
                energyRegenTimer = 0;
            }

            // Limit energyCurrent from going over the limit imposed by energyMax.
            energyCurrent = Utils.Clamp(energyCurrent, 0, energyMax2);
        }

        private void CapResourceGodMode()
        {
            if (Main.myPlayer == Player.whoAmI && Player.creativeGodMode)
            {
                energyCurrent = energyMax2;
            }
        }
    }
}