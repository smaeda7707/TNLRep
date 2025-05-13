using Microsoft.Xna.Framework;
using NewLoot.Content.Items;
using System.Drawing.Drawing2D;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NewLoot.Common.Players;
using NewLoot.Content.Buffs;
using System.Threading;
using System.Timers;
using System.Data;

namespace NewLoot.Content.Items.Accessories
{
    public class PureTotem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.value = Item.buyPrice(0, 3, 50);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PureTotemPlayer>().equipped = true;

            player.GetDamage(DamageClass.SummonMeleeSpeed) += 0.40f;
            player.GetKnockback(DamageClass.SummonMeleeSpeed) += 0.20f;


            OceanWhipDebuff.TagDamage = 0;
            LeafWhipDebuff.TagDamage = 0;
            DungeonWhipDebuff.TagDamage = 0;
        }

    }
    public class PureTotemPlayer : ModPlayer
    {
        // The fields related to the dash accessory
        public bool equipped;
        private int timer = 0;
        public override void ResetEffects()
        {
            timer = MakeSureEquipWork();
            OceanWhipDebuff.TagDamage = 4;
            LeafWhipDebuff.TagDamage = 5;
            DungeonWhipDebuff.TagDamage = 7;
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
    }
}
