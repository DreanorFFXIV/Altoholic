using System;
using System.Collections.Generic;
using System.Numerics;
using Altoholic.Data;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    public List<CharacterContainer> CharacterContainers { get; set; }

    public MainWindow() 
        : base("Altoholic", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose()
    {
    }

    public override void Draw()
    {
        ImGui.Spacing();
        
        ImGui.BeginChild("ScrollableRegion", new Vector2(0, 0), true, ImGuiWindowFlags.HorizontalScrollbar);

        CurrencyWindow.Draw(CharacterContainers);
        
        ImGui.EndChild();
    }
}
