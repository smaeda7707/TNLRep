using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Common.Players;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class HemoshroomShoes : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 4;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CrimtanePowder>(), 21);
            recipe.AddIngredient(ItemID.TissueSample, 14);
            recipe.AddIngredient(ItemID.GlowingMushroom, 12);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "When below half life, rapidly restore life at a cost of 1 energy per 2 life";

            if (player.statLife <= player.statLifeMax/2 && player.GetModPlayer<Energy>().energyCurrent > 0)
            {
                player.statLife += 2;
                player.GetModPlayer<Energy>().energyCurrent -= 1;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<HemoshroomHat>() && body.type == ModContent.ItemType<HemoshroomChest>())
            {
                return true;
            }
            return false;
        }
    }
    
    
}
