using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using System.Threading;

namespace NewLoot.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class DemonMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.buyPrice(0, 1, 20);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= player.statLifeMax/2)
            {
                player.GetDamage(DamageClass.Generic) += 0.15f;
            }
            player.moveSpeed -= 0.03f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ebonwood, 60)
                .AddIngredient(ModContent.ItemType<DemonitePowder>(), 12)
                .AddIngredient(ItemID.WrathPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

    }
}
