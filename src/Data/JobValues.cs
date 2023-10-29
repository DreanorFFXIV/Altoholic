using System.Collections.Generic;

namespace Altoholic.Data;

public static class JobValues
{
    public static readonly List<uint> Dps = new() {22, 23, 25, 27, 30, 31, 20, 34, 35, 36, 38, 39};
    public static readonly List<uint> Heal = new() {24, 28, 33, 40};
    public static readonly List<uint> Tank = new() {19, 21, 32, 37};
    public static readonly List<uint> Gather = new() {16, 17, 18};
    public static readonly List<uint> Craft = new() {8, 9, 10, 11, 12, 13, 14, 15};
    
    public static int GetSortIndex(uint value)
    {
        return Tank.Contains(value) ? 1 :
            Dps.Contains(value) ? 2 :
            Heal.Contains(value) ? 3 :
            Craft.Contains(value) ? 4 :
            Gather.Contains(value) ? 5 : 0;
    }
}