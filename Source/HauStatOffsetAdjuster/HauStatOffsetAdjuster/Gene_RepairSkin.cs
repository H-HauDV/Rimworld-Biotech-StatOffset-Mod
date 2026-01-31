using System.Collections.Generic;
using Verse;

namespace HauStatOffsetAdjuster
{
    public class Gene_RepairSkin : Gene
    {
        public override void Tick()
        {
            int repairInterval = RepairSkinMod.settings.repairInterval;
            if (!pawn.IsHashIntervalTick(repairInterval + pawn.thingIDNumber % 500))
            {
                return;
            }

            TryRepairAllEquippedItems();
        }

        private void TryRepairAllEquippedItems()
        {
            int repairAmount = RepairSkinMod.settings.repairAmount;

            List<Thing> repairableItems = new List<Thing>();
            if (pawn.apparel != null && pawn.apparel.WornApparelCount > 0)
            {
                repairableItems.AddRange(pawn.apparel.WornApparel);
            }
            if (pawn.equipment?.Primary != null)
            {
                repairableItems.Add(pawn.equipment.Primary);
            }
            if (repairableItems.Count == 0)
            {
                return;
            }

            foreach (Thing item in repairableItems)
            {
                if (item.def.useHitPoints && item.HitPoints < item.MaxHitPoints)
                {
                    item.HitPoints = System.Math.Min(item.HitPoints + repairAmount, item.MaxHitPoints);
                }
            }
        }
    }
}
