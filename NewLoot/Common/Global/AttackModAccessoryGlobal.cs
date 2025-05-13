using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Content.Items.Weapons;
using NewLoot.Content.Items.Armor;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using Terraria.Audio;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Projectiles;
using NewLoot.Content.Tiles;

namespace NewLoot.Common.Global
{
    internal class AttackModAccessoryGlobal : GlobalItem
    {
        public static int manaSyphonAmount = 2;

        public override bool? UseItem(Item item, Player player)
        {
            if (player.altFunctionUse == 2)
            {

                // Desert Lamp
                if (player.GetModPlayer<DesertLampPlayer>().desertLampEquipped)
                {
                    Vector2 velocity = new Vector2(0, 0);
                    int damage;
                    if (item.GetGlobalItem<GlobalFields>().energyCost < 4)
                    {
                        damage = 4;
                    }
                    else
                    {
                        damage = item.GetGlobalItem<GlobalFields>().energyCost;
                    }
                    Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.position, velocity, ModContent.ProjectileType<MagicLampDust>(), damage, 3.2f, player.whoAmI);
                }


                // Sub Zero
                if (player.GetModPlayer<SubZeroBuffPlayer>().subZerod)
                {
                    int damage;
                    if (item.GetGlobalItem<GlobalFields>().energyCost > 9){
                        damage = item.GetGlobalItem<GlobalFields>().energyCost * 8;

                        float vect1 = 15f;
                        float vect2 = 15f;

                        for (int i = 0; i < 8; i++)
                        {
                            // Random upward vector.
                            Vector2 velocity = new Vector2(vect1, vect2);
                            // Importantly, ai1 is set to 1 here. This is checked in OnTileCollide to prevent bouncing and here in Kill to prevent an infinite chain of splitting projectiles.
                            Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.position, velocity, ModContent.ProjectileType<SubZeroShard>(), damage, 3.2f, player.whoAmI);

                            if (i == 1)
                            {
                                vect1 = -15f;
                                vect2 = 15f;
                            }
                            else if (i == 2)
                            {
                                vect1 = 15f;
                                vect2 = -15f;
                            }
                            else if (i == 3)
                            {
                                vect1 = -15f;
                                vect2 = -15f;
                            }
                            else if (i == 4)
                            {
                                vect1 = -21f;
                                vect2 = 0;
                            }
                            else if (i == 5)
                            {
                                vect1 = 21f;
                                vect2 = 0;
                            }
                            else if (i == 6)
                            {
                                vect1 = 0;
                                vect2 = 21f;
                            }
                            else
                            {
                                vect1 = 0;
                                vect2 = -21f;
                            }
                        }
                    }
                }


            }


            return base.UseItem(item, player);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Frenzy Glove
            if (player.GetModPlayer<FrenzyGlovePlayer>().equipped)
            {
                if (item.DamageType == DamageClass.Melee)
                {
                    player.AddBuff(ModContent.BuffType<Exhilaration>(), 60);
                }
            }

            // Mana Syphon
            if (player.GetModPlayer<ManaSyphonPlayer>().equipped)
            {
                if (item.DamageType == DamageClass.Magic)
                {
                    player.statMana += manaSyphonAmount;
                }
            }

            // Scale Fossil Set Bonus
            if (player.GetModPlayer<ScaleFossilPlayer>().equipped)
            {
                ScaleFossilPlayer.scaleFossilSetDamage += damageDone;
            }
        }
    }
    internal class AttackProjAccessoryGlobal : GlobalProjectile
    {

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Frost Chain
            if (Main.player[projectile.owner].GetModPlayer<FrostChainPlayer>().equipped)
            {
                if (projectile.DamageType == DamageClass.SummonMeleeSpeed)
                {
                    if (Main.rand.NextBool(6))
                    {
                        target.AddBuff(BuffID.Frostburn, 180);
                    }
                }
            }
            // Frenzy Glove
            if (Main.player[projectile.owner].GetModPlayer<FrenzyGlovePlayer>().equipped)
            {
                if (projectile.DamageType == DamageClass.Melee || projectile.DamageType == DamageClass.MeleeNoSpeed)
                {
                    Main.player[projectile.owner].AddBuff(ModContent.BuffType<Exhilaration>(), 60);
                }
            }
            // Mana Syphon
            if (Main.player[projectile.owner].GetModPlayer<ManaSyphonPlayer>().equipped)
            {
                if (projectile.DamageType == DamageClass.Magic)
                {
                    Main.player[projectile.owner].statMana += AttackModAccessoryGlobal.manaSyphonAmount;
                }
            }

            // Scale Fossil Set Bonus
            if (Main.player[projectile.owner].GetModPlayer<ScaleFossilPlayer>().equipped)
            {
                ScaleFossilPlayer.scaleFossilSetDamage += damageDone;
            }
        }
    }
}
