using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;

namespace NewLoot.Content.Items.Accessories
{
    public class CastingScroll : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.value = Item.buyPrice(0, 3, 80);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.08f;
            player.GetAttackSpeed(DamageClass.Magic) += 0.08f;
        }

    }
}
