using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Common.Math;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class Job
{
    public string Name { get; }
    
    public short Level { get; }
    
    [JsonIgnore]
    public int Sort { get; set; }

    public Job(string name, short level)
    {
        Name = name;
        Level = level;
    }
}