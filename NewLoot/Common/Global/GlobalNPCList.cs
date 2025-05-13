using Newloot.Content.Items.Weapons;
using NewLoot.Common.ItemDropRules;
using NewLoot.Content.Items;
using NewLoot.Content.Items.Accessories;
using NewLoot.Content.Items.Armor;
using NewLoot.Content.Items.Weapons;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.RGB;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewLoot.Content.Global
{
    internal class GlobalNPCList : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Ice Wand
            if (npc.type == NPCID.IceSlime || npc.type == NPCID.IceElemental)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.InRain.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<IceWand>(), 33));
            }

            // Ice Staff
            if (npc.type == NPCID.SpikedIceSlime || npc.type == NPCID.IceBat)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<IceStaff>(), 40));
            }
            // Sun Rod + Knife
            if (npc.type == NPCID.Antlion || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.FlyingAntlion)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<SunRod>(), 40));
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<SunKnife>(), 40));
            }

            // Wind Cannon + Cyclone Sickle
            if (npc.type == NPCID.Harpy)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new EOW_WindyCondition(), ModContent.ItemType<WindCannon>(), chanceDenominator: 50, chanceNumerator: 1));
                npcLoot.Add(ItemDropRule.ByCondition(new EOW_WindyCondition(), ModContent.ItemType<CycloneSickle>(), chanceDenominator: 50, chanceNumerator: 1));
            }


            // Ancient Scimitar
            if (npc.type == NPCID.Tumbleweed)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.InSandstorm.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<AncientScimitar>(), 20));
            }
            else if (npc.type == NPCID.SandSlime || npc.type == NPCID.Vulture || npc.type == NPCID.Zombie || npc.type == NPCID.TorchZombie || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.FlyingAntlion || npc.type == NPCID.Antlion || npc.type == NPCID.Mummy)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.InSandstorm.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<AncientScimitar>(), 30));
            }

            // Rain Spear
            if (npc.type == NPCID.UmbrellaSlime|| npc.type == NPCID.FlyingFish || npc.type == NPCID.ZombieRaincoat)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.InRain.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<RainSpear>(), 40));
            }
            else if (npc.type == NPCID.BlueSlime || npc.type == NPCID.GreenSlime || npc.type == NPCID.Zombie|| npc.type == NPCID.SlimedZombie || npc.type == NPCID.DemonEye || npc.type == NPCID.WanderingEye || npc.type == NPCID.SlimeMasked)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.InRain.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<RainSpear>(), 50));
            }

            // Bay Blade
            if (npc.type == NPCID.PinkJellyfish || npc.type == NPCID.Crab || npc.type == NPCID.Shark)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BayBlade>(), 40));
            }
            if (npc.type == NPCID.Squid)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BayBlade>(), 33));
            }
            if (npc.type == NPCID.SeaSnail)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BayBlade>(), 20));
            }

            // Bubble Yoyo
            if (npc.type == NPCID.PinkJellyfish || npc.type == NPCID.Crab || npc.type == NPCID.Shark)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BubbleYoyo>(), 40));
            }
            if (npc.type == NPCID.Squid)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BubbleYoyo>(), 33));
            }
            if (npc.type == NPCID.SeaSnail)
            {
                npcLoot.Add(ItemDropRule.ByCondition(Condition.DownedEyeOfCthulhu.ToDropCondition(ShowItemDropInUI.Always), ModContent.ItemType<BubbleYoyo>(), 20));
            }

            // Snatcher Jaw
            if (npc.type == NPCID.Snatcher)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SnatcherJaw>(), 12));
            }





            // BOSS LOOT MOD FOR NORMAL MODE


            //Queen Bee
            if (npc.type == NPCID.QueenBee)
            {
                foreach (var rule in npcLoot.Get())
                {
                    if (rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.BeeKeeper))
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Remove(ItemID.BeeKeeper);
                        original.Remove(ItemID.BeeGun);
                        original.Remove(ItemID.BeesKnees);
                        original.Add(ModContent.ItemType<HoneyLance>());
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
            }

        }





        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add(new Item(ModContent.ItemType<ForestHood>())
                {
                    shopCustomPrice = 100000,
                });
                shop.Add(new Item(ModContent.ItemType<ForestChestplate>())
                {
                    shopCustomPrice = 100000,
                });
                shop.Add(new Item(ModContent.ItemType<ForestBoots>())
                {
                    shopCustomPrice = 100000,
                });
                shop.Add(new Item(ModContent.ItemType<LeafWhip>())
                {
                    shopCustomPrice = 135000,
                });
            }
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(new Item(ModContent.ItemType<RustyBow>())
                {
                    shopCustomPrice = 150000,
                });
            }
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(new Item(ModContent.ItemType<NitroCapsule>())
                {
                    shopCustomPrice = 80000,
                });
            }
            if (shop.NpcType == NPCID.WitchDoctor)
            {
                shop.Add(new Item(ModContent.ItemType<PureTotem>())
                {
                    shopCustomPrice = 100000,
                });
            }
            if (shop.NpcType == NPCID.Mechanic)
            {
                shop.Add(new Item(ModContent.ItemType<TechBlade>())
                {
                    shopCustomPrice = 180000,
                });
                shop.Add(new Item(ModContent.ItemType<BoostBoots>())
                {
                    shopCustomPrice = 120000,
                });
                shop.Add(new Item(ModContent.ItemType<CoolingPack>())
                {
                    shopCustomPrice = 120000,
                });
            }
            if (shop.NpcType == NPCID.Clothier)
            {
                shop.Add(new Item(ModContent.ItemType<CozyScarf>())
                {
                    shopCustomPrice = 50000,
                });
            }
            if (shop.NpcType == NPCID.GoblinTinkerer)
            {
                shop.Add(new Item(ModContent.ItemType<ModernBow>())
                {
                    shopCustomPrice = 100000,
                });
            }

            if (shop.FullName != NPCShopDatabase.GetShopName(NPCID.BestiaryGirl, "Shop"))
                return;
            shop.Add(ItemID.Gel, Condition.DownedKingSlime);

        }

    }
}
