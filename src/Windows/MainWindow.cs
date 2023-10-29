using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Altoholic.Data;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    private bool _overViewIsSelected;
    private string _selectedCharacter;
    private readonly ICommandManager _commandManager;

    public List<CharacterContainer> CharacterContainers { get; set; }

    public MainWindow(ICommandManager commandManager, string name)
        : base(name, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
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
        ImGui.BeginChild("LeftChild", new Vector2(200, 0), true);
        {
            if (ImGui.Selectable("Overview", _overViewIsSelected))
            {
                _overViewIsSelected = true;
                _selectedCharacter = string.Empty;
            }

            DrawCharacterSelection();

            ImGui.EndChild();
        }
        
        ImGui.SameLine();
        
        ImGui.BeginChild("RightChild", new Vector2(0, 0), true);
        {
            if (_overViewIsSelected)
            {
                OverviewWindow.Draw(CharacterContainers, _commandManager);
            }
            else
            {
                if (!string.IsNullOrEmpty(_selectedCharacter))
                {
                    var temp = new List<CharacterContainer> { CharacterContainers.First(x => x.Name == _selectedCharacter) };
                    CurrencyWindow.Draw(temp);
                    CollectionWindow.Draw(temp);
                }
            }

            ImGui.EndChild();
        }
    }

    private void DrawCharacterSelection()
    {
        ImGui.Spacing();
        ImGui.Spacing();
        ImGui.Text("Characters");
        ImGui.Separator();
        
        foreach (var character in CharacterContainers
                     .GroupBy(x => x.Name.Remove(0, x.Name.IndexOf("@") + 1))
                     .OrderBy(x => x.Key))
        {
            if (!ImGui.TreeNode(character.Key)) continue;
            foreach (var currentChar in character.OrderBy(x => x.Name))
            {
                if (ImGui.Selectable(currentChar.Name, !string.IsNullOrEmpty(_selectedCharacter) && _selectedCharacter == currentChar.Name))
                {
                    _overViewIsSelected = false;
                    _selectedCharacter = currentChar.Name;
                }
            }

            ImGui.TreePop();
        }
        
        ImGui.Separator();
    }
}