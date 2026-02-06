using Verse;
using UnityEngine;

namespace HauStatOffsetAdjuster
{
    public class RepairSkinModSettings : ModSettings
    {
        public int repairInterval = 60000; // Default: Once per in-game day
        public int repairAmount = 200;     // Default: Repairs 200 HP

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref repairInterval, "repairInterval", 60000);
            Scribe_Values.Look(ref repairAmount, "repairAmount", 200);
        }
    }

    public class RepairSkinMod : Mod
    {
        public static RepairSkinModSettings settings;

        public RepairSkinMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<RepairSkinModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("Repair Interval (ticks): " + settings.repairInterval);
            settings.repairInterval = (int)listing.Slider(settings.repairInterval, 1000, 120000); // Min: 1000, Max: 2 in-game days

            listing.Label("Repair Amount: " + settings.repairAmount);
            settings.repairAmount = (int)listing.Slider(settings.repairAmount, 10, 500); // Min: 10, Max: 500 HP

            listing.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Hau stats offset repair Skin Settings";
        }
    }
}
