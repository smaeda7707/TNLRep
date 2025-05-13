using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace NewLoot.Content.Tiles
{
    internal class MagtaneOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true; // ore merges w dirt blocks (HAVE TO MAKE TEXTURE FOR IT)
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileShine2[Type] = true; // Makes tile drawn slightly more bright
            Main.tileSpelunker[Type] = true; // Spelunker Potion works on tile
            Main.tileOreFinderPriority[Type] = 350; // Metal Detector Priority

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(152, 171, 198), name);

            DustType = DustID.Flare_Blue; // Placeholder
            HitSound = SoundID.Tink;

            MineResist = 1f;
            MinPick = 35;
        }
    }
}
