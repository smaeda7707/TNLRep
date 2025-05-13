using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using System.Threading;
using Terraria.Audio;

namespace NewLoot.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class VigilanteScarf : ModItem
    {
        private int energyCost = 30;
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.buyPrice(0, 5, 90);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= 40 && !player.HasBuff(ModContent.BuffType<Shadow>()) && player.GetModPlayer<Energy>().energyCurrent >= energyCost)
            {
                SoundEngine.PlaySound(SoundID.Item103, player.Center);
                player.AddBuff(ModContent.BuffType<Shadow>(), 360);
                player.GetModPlayer<Energy>().energyCurrent -= energyCost;
                player.GetModPlayer<ShadowEyePlayer>().drawEye = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 12);
            recipe.AddIngredient(ModContent.ItemType<BoneMarrow>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CozyScarf>());
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
