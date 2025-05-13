using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using NewLoot.Content.Tiles;

namespace NewLoot.Content.Items
{
    internal class MagtaneOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;

            // This ore can spawn in slime bodies like other pre-boss ores. (copper, tin, iron, etch)
            // It will drop in amount from 3 to 13.
            ItemID.Sets.OreDropsFromSlime[Type] = (3, 13);
        }
        public override void SetDefaults()
        {
            //Size of Item
            Item.width = 16;
            Item.height = 16;

            Item.value = Item.buyPrice (silver: 1);
            Item.maxStack = 9999; //The max amount of Item you can have in 1 stack
            Item.rare = ItemRarityID.White;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.MagtaneOreTile>());
        }
    }
}
