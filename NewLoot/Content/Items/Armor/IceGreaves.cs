using Terraria.ID;
using Terraria;
using System;
using Terraria.ModLoader;
using NewLoot.Content.Items;
using NewLoot.Content.Buffs;
using Terraria.Audio;

namespace NewLoot.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class IceGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 5;
            Item.value = 13500;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UpdateEquip(Player player)
        {
            if (player.statLife >= player.statLifeMax)
            {
                player.endurance += 0.05f;
            }
            else
            {
                player.moveSpeed += 0.07f;
            }

            player.moveSpeed -= 0.04f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 12);
            recipe.AddIngredient(ItemID.IceBlock, 150);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "6 defense\n25% chance for armor to crack when taking more than 18 damage";
            if (!player.HasBuff(ModContent.BuffType<IceArmorDebuff>())) {
                player.statDefense += 6;
            }
            player.GetModPlayer<IceArmorPlayer>().IceSetBonus = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<IceMask>() && body.type == ModContent.ItemType<IceChestplate>())
            {
                return true;
            }
            return false;
        }
    }
    public class IceArmorPlayer : ModPlayer
    {
        public bool IceSetBonus;
        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
            IceSetBonus = false;
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            IceArmorSetEffect(hurtInfo);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            IceArmorSetEffect(hurtInfo);
        }

        public void IceArmorSetEffect(Player.HurtInfo hurtInfo)
        {
            if (IceSetBonus == true && hurtInfo.Damage > 18)
            {
                if (Main.rand.NextBool(4))
                {
                    Player.AddBuff(ModContent.BuffType<IceArmorDebuff>(), 1040);
                    SoundEngine.PlaySound(SoundID.Item27, Player.position);

                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Ice_Purple, 0f, 0f, 100, default, 2f);
                        dust.velocity *= 0.3f;
                        dust.noGravity = true;
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Ice, 0f, 0f, 100, default, 3f);
                        dust.noGravity = true;
                        dust.velocity *= 1.1f;
                        dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Ice, 0f, 0f, 100, default, 2f);
                        dust.velocity *= 0.6f;
                    }
                }
            }
        }
    }
}
