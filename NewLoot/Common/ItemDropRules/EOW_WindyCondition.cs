using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace NewLoot.Common.ItemDropRules
{
    internal class EOW_WindyCondition : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public EOW_WindyCondition()
        {
            Description ??= Language.GetOrRegister("Mods.NewLoot.DropConditions.Example");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.IsItAHappyWindyDay && (NPC.downedBoss2 || NPC.downedQueenBee);
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return Description.Value;
        }
    }
}
