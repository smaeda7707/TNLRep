using NewLoot.Content.Items.Armor;
using NewLoot.Content.Items.Weapons;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;

namespace Newloot.Common
{
    public class GlobalBossBagLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            // This is for boss bags

            // Queen bee
            if (item.type == ItemID.QueenBeeBossBag)
            {
                foreach (var rule in itemLoot.Get())
                {
                    if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.BeeKeeper))
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

            // King Slime
            if (item.type == ItemID.KingSlimeBossBag)
            {
                foreach (var rule in itemLoot.Get())
                {
                    if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.SlimeHook))
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Remove(ItemID.SlimeHook);
                        original.Remove(ItemID.SlimeGun);
                        original.Add(ModContent.ItemType<SlimeMace>());
                        original.Add(ModContent.ItemType<ModifiedSlimeGun>());
                        original.Add(ModContent.ItemType<SlimeLauncher>());
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
            }










            // Halloween bags    DOES NOT WORK
            if (item.type == ItemID.GoodieBag)
            {
                foreach (var rule in itemLoot.Get())
                {
                    if (rule is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop && oneFromOptionsDrop.dropIds.Contains(ItemID.UnluckyYarn) && NPC.downedBoss1)
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Add(ModContent.ItemType<GrimChestplate>());
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
            }
        }
    }
}
