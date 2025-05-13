using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class MythrilHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 13;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.10f;
            player.statManaMax2 += 40;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 12);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Converts every weapon to a magic weapon\nMana usage scales with the weapons base damage";
            player.GetModPlayer<MythrilPlayer>().setBonusOn = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<MythrilBoots>() && body.type == ModContent.ItemType<MythrilChest>())
            {
                return true;
            }
            return false;
        }

        public class MythrilPlayer : ModPlayer
        {
            public bool setBonusOn;

            public override void UpdateEquips()
            {
                if (Player.head != ModContent.ItemType<MythrilHelm>() || Player.chest != ModContent.ItemType<MythrilChest>() || Player.legs != ModContent.ItemType<MythrilBoots>())
                {
                    setBonusOn = false;
                }
            }
        }
    }
}
