using Ardalis.SmartEnum;

namespace UrbanEngine.Core.Enums
{
    public class RegionType : SmartEnum<RegionType, int>
    {
        // US Regions
        public static RegionType SouthernRegion = new RegionType("Southern", 1);
        public static RegionType SouthwestRegion = new RegionType("Southwest", 2);
        public static RegionType MidAtlanticRegion = new RegionType("Mid-Atlantic", 3);
        public static RegionType NewEnglandRegion = new RegionType("New England", 4);
        public static RegionType PacificCoastalRegion = new RegionType("Pacific Coastal", 5);
        public static RegionType RockyMountainRegion = new RegionType("Rocky Mountain", 6);
        public static RegionType MidwestRegion = new RegionType("Midwest", 7);

        public RegionType(string name, int value) 
            : base(name, value) { }
    }
}
