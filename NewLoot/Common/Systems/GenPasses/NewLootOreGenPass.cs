using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.WorldBuilding;
using NewLoot.Content.Tiles;

namespace NewLoot.Common.Systems.GenPasses
{
    internal class NewLootOreGenPass : GenPass
    {
        public NewLootOreGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Adding Corite, Magtane, and Kanum";

            // CoriteOre
            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 7E-05);
            for (int i = 0; i < maxToSpawn; i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(3, 7), ModContent.TileType<CoriteOreTile>());

            }

            // MagtaneOre
            int maxToSpawn2 = (int)(Main.maxTilesX * Main.maxTilesY * 7E-05);
            for (int i = 0; i < maxToSpawn2; i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(3, 7), ModContent.TileType<MagtaneOreTile>());

            }
            // KanumOre
            int maxToSpawn3 = (int)(Main.maxTilesX * Main.maxTilesY * 7E-05);
            for (int i = 0; i < maxToSpawn3; i++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(3, 7), ModContent.TileType<KanumOreTile>());

            }
        }
    }
}
