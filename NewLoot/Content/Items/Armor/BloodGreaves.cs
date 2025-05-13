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
    internal class BloodGreaves : ModItem
    {
        private int setEnergyCost = 5;
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 5;
            player.moveSpeed -= 0.06f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 12);
            recipe.AddIngredient(ItemID.SilverBar, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to guarentee a critical strike on the next hit\nUses 5 energy";
            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] < 15 && !player.HasBuff(ModContent.BuffType<BloodSetBuff>()) && player.GetModPlayer<Energy>().energyCurrent >= setEnergyCost)
            {
                player.AddBuff(ModContent.BuffType<BloodSetBuff>(), 600);
                player.GetModPlayer<Energy>().energyCurrent -= setEnergyCost;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<BloodHelmet>() && body.type == ModContent.ItemType<BloodChestplate>())
            {
                return true;
            }
            return false;
        }
    }

    internal class BloodSetGlobal : GlobalItem
    {
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.HasBuff(ModContent.BuffType<BloodSetBuff>()))
            {
                modifiers.SetCrit();
                player.ClearBuff(ModContent.BuffType<BloodSetBuff>());
            }
        }
    }

    internal class BloodSetProjGlobal : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<BloodSetBuff>()))
            {
                modifiers.SetCrit();
                Main.player[projectile.owner].ClearBuff(ModContent.BuffType<BloodSetBuff>());
            }
        }
    }
}
