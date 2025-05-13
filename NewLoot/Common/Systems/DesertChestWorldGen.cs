using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.WorldBuilding;
using NewLoot.Content.Tiles;
using Terraria.ID;
using Newloot.Content.Items.Weapons;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Items.Armor;

namespace NewLoot.Common.Systems.GenPasses
{
    public class DesertChestWorldGen : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Place some additional items in Water Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWaterChests = { ModContent.ItemType<AncientChestplate>(), ModContent.ItemType<DesertTrinket>(), ModContent.ItemType<DesertLamp>()};
            // This variable will help cycle through the items so that different Water Chests get different items
            int itemsToPlaceInWaterChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each). 
            int itemsPlaced = 0;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the  Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the  Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the  Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/_Chest
                if (chestTile.TileType == TileID.Containers2 && chestTile.TileFrameX == 10 * 36) // MAKE SURE TO USE Containers2 FOR CHESTS IN CHEST LIST 2
                {
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.ThunderSpear || chest.item[inventoryIndex].type == ItemID.ThunderStaff || chest.item[inventoryIndex].type == ItemID.SandBoots || chest.item[inventoryIndex].type == ItemID.MysticCoilSnake)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWaterChests[itemsToPlaceInWaterChestsChoice]);
                            // Decide on the next item that will be placed.
                            itemsToPlaceInWaterChestsChoice = (itemsToPlaceInWaterChestsChoice + 1) % itemsToPlaceInWaterChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
            }
        }
    }
}
