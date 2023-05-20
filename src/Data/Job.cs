using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace Altoholic.Data;

public class Job
{
    public string Name { get; } = string.Empty;
    
    public short Level { get; }

    public Vector4 Color { get; } = new Vector4(0,0,0,0);
    
    public int Sort { get; }
    
    public Job(string name, short level, Vector4 color, int sort)
    {
        Name = name;
        Level = level;
        Color = color;
        Sort = sort;
    }
}