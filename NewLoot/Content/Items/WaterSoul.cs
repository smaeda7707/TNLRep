using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;
using Terraria.DataStructures;

namespace NewLoot.Content.Items
{
    internal class WaterSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity

            Item.ResearchUnlockCount = 15; // Configure the amount of this item that's needed to research it in Journey mode.
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 20;
            Item.height = 18;

            Item.value = Item.buyPrice (silver: 5);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.Blue;
        }
    }
    
}
