using System;
using System.Collections.Generic;
using System.Numerics;
using Altoholic.Data;
using Altoholic.Data.Currency;
using Dalamud.Data;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    public List<CharacterContainer> CharacterContainers { get; set; }

    private CommandManager _commandManager;
    public MainWindow(CommandManager commandManager) 
        : base("Altoholic", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        _commandManager = commandManager;
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
        
        OverviewWindow.Draw(CharacterContainers, _commandManager);
        CurrencyWindow.Draw(CharacterContainers);
        
        ImGui.EndChild();
    }
}
