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
    internal class CobaltHood : ModItem
    {
        private int setEnergyCost = 38;
        public override void SetDefaults()
        {
            Item.defense = 8;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed -= 0.02f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.07f;
            player.runAcceleration += 0.08f;
            Lighting.AddLight(player.Top, new Color(176, 248, 255).ToVector3() * 0.2f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 12);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Double tap down to create a slowing zone around you for 15 seconds\nUses 38 energy";


            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] < 15 && player.GetModPlayer<Energy>().energyCurrent >= setEnergyCost && player.ownedProjectileCounts[ModContent.ProjectileType<CobaltSetZone>()] < 1)
            {
                SoundEngine.PlaySound(SoundID.Item114, player.Center);
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<CobaltSetZone>(), 0, 0, player.whoAmI);
                player.GetModPlayer<Energy>().energyCurrent -= setEnergyCost;
            }

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (legs.type == ModContent.ItemType<CobaltPants>() && body.type == ModContent.ItemType<CobaltShirt>())
            {
                return true;
            }
            return false;
        }

    }
}
