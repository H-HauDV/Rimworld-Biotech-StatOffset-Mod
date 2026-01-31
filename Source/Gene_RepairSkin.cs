using System.Collections.Generic;
using RimWorld;
using Verse;

namespace HauStatOffsetAdjuster
{
    public class Gene_RepairSkin : Gene
    {
        public override void Tick()
        {
            // Run every 30,000 ticks (twice per in-game day)
            if (!pawn.IsHashIntervalTick(30000))
            {
                return;
            }

            TryRepairAllEquippedItems();
        }

        private void TryRepairAllEquippedItems()
        {
            // Gather all items in one list to minimize looping
            List<Thing> repairableItems = new List<Thing>();
            // Collect worn apparel
            if (pawn.apparel != null && pawn.apparel.WornApparelCount > 0)
            {
                repairableItems.AddRange(pawn.apparel.WornApparel);
            }
            // Collect primary weapon
            if (pawn.equipment?.Primary != null)
            {
                repairableItems.Add(pawn.equipment.Primary);
            }
            // Exit early if there is nothing to repair
            if (repairableItems.Count == 0)
            {
                return;
            }
            // Repair each item if needed
            foreach (Thing item in repairableItems)
            {
                if (item.def.useHitPoints && item.HitPoints < item.MaxHitPoints)
                {
                    item.HitPoints = System.Math.Min(item.HitPoints + 100, item.MaxHitPoints);
                }
            }
        }
    }
}
