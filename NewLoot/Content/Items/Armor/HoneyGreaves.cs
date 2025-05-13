using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Common.Players;
using Terraria.DataStructures;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class HoneyGreaves : ModItem
    {
        public int equip = -1;
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 2;
            player.moveSpeed -= 0.05f;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledHoney, 18);
            recipe.AddIngredient(ItemID.BeeWax, 10);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Hold jump to fly with bee wings\nFlying with wings drains 2 energy per second";

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<HoneyHelm>() && body.type == ModContent.ItemType<HoneyPlate>())
            {
                return true;
            }
            return false;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
        ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f; // Falling glide speed
            ascentWhenRising = 0.15f; // Rising speed
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                //equip = EquipLoader.AddEquipTexture(Mod, "Accessory_Back_29", EquipType.Back, this);
                equip = EquipLoader.AddEquipTexture(Mod, "NewLoot/Content/Items/Armor/HoneyWings_Wings", EquipType.Wings, this);
            }
        }
    }
}
