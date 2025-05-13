using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ObjectData;

namespace NewLoot.Content.Tiles
{
    internal class KanumBarTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            DustType = DustID.Copper; // Placeholder

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.KanumBar")); // localized text for "Metal Bar"
        }
    }
    
}
