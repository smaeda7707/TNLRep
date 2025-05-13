using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using Microsoft.Xna.Framework;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class AdamantiteHead : ModItem
    {
        private int setEnergyCost = 24;
        public override void SetDefaults()
        {
            Item.defense = 10;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.04f;
            player.GetCritChance(DamageClass.Generic) += 10;
            Lighting.AddLight(player.Top, new Color(176, 248, 255).ToVector3() * 0.2f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 10);
            recipe.AddIngredient(ItemID.PlatinumBar, 6);
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            Vector2 pos = player.Top - new Vector2(10, -10);
            player.setBonus = "Double tap down to fire a laser out of your visor for 10 seconds\nUses 24 energy";

            int timer = player.GetModPlayer<NewLootPlayer>().adamantiteSetTimer;

            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] < 15 && timer <= 0 && player.GetModPlayer<Energy>().energyCurrent >= setEnergyCost)
            {
                SoundEngine.PlaySound(SoundID.Item114, player.Center);
                player.GetModPlayer<NewLootPlayer>().adamantiteSetTimer = 600;
                player.GetModPlayer<Energy>().energyCurrent -= setEnergyCost;
            }

            if (timer > 0)
            {
                if (timer % 8 == 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), pos, (Main.MouseWorld - pos).SafeNormalize(Vector2.Zero) * 2, ModContent.ProjectileType<AdamantiteSetLaser>(), 22, 0);
                }
                else
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), pos, (Main.MouseWorld - pos).SafeNormalize(Vector2.Zero) * 2, ModContent.ProjectileType<AdamantiteSetLaser>(), 0, 0);
                }
                player.GetModPlayer<NewLootPlayer>().adamantiteSetTimer--;
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<AdamantiteLegs>() && body.type == ModContent.ItemType<AdamantiteChest>())
            {
                return true;
            }
            return false;
        }

    }
}
