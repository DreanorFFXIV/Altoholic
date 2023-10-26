using System;
using System.Collections.Generic;
using System.Numerics;
using Altoholic.Data;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    public List<CharacterContainer> CharacterContainers { get; set; }

    private ICommandManager _commandManager;
    public MainWindow(ICommandManager commandManager) 
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
        CollectionWindow.Draw(CharacterContainers);
        
        ImGui.EndChild();
    }
}
