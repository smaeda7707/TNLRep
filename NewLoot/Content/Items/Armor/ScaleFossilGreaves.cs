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
    internal class ScaleFossilGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 13500;
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 2;
            player.moveSpeed -= 0.02f;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FossilOre, 18);
            recipe.AddIngredient(ItemID.ShadowScale, 14);
            recipe.AddIngredient(ItemID.Amber, 4);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Decreased damage by 10%\nDealing damage increases attack up to 30%\nTaking more than 15 damage resets the damage bonus";
            player.GetModPlayer<ScaleFossilPlayer>().equipped = true;
            player.GetDamage(DamageClass.Generic) -= 0.10f;
            player.GetDamage(DamageClass.Generic) += (ScaleFossilPlayer.scaleFossilSetDamage/9999);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<ScaleFossilHelm>() && body.type == ModContent.ItemType<ScaleFossilPlate>())
            {
                return true;
            }
            return false;
        }
    }

    public class ScaleFossilPlayer : ModPlayer
    {
        public static float scaleFossilSetDamage;
        private int timer = 0;

        // The fields related to the dash accessory
        public bool equipped;

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            timer = MakeSureEquipWork();
        }
        public int MakeSureEquipWork()
        {
            if (timer == 0)
            {
                timer++;
            }
            else
            {
                timer = 0;
                equipped = false;
            }
            return timer;
        }

        public override void UpdateEquips()
        {
            if (scaleFossilSetDamage > 3000)
            {
                scaleFossilSetDamage = 3000;
            }
            if (scaleFossilSetDamage == 3000 && equipped)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.DemonTorch, 0f, 0f, 100, default, 2f);
                    dust.velocity *= 0.3f;
                    dust.noGravity = true;
                }
            }

        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (hurtInfo.Damage > 15)
            {
                scaleFossilSetDamage = 0;
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (hurtInfo.Damage > 15)
            {
                scaleFossilSetDamage = 0;
            }
        }
    }
}
