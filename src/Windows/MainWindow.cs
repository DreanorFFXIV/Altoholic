using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Altoholic.Data;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    private bool overViewIsSelected;
    private int selectedCharacterIndex = -1;
    private ICommandManager _commandManager;

    public List<CharacterContainer> CharacterContainers { get; set; }

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
        DrawSelection();
        DrawContent();
    }

    private void DrawSelection()
    {
        ImGui.BeginChild("LeftChild", new Vector2(200, 0), true);

        if (ImGui.Selectable("Overview", overViewIsSelected))
        {
            overViewIsSelected = true;
            selectedCharacterIndex = -1;
        }

        ImGui.Spacing();
        ImGui.Spacing();
        ImGui.Text("Characters");
        ImGui.Separator();

        var index = 0;
        foreach (var character in CharacterContainers.GroupBy(x => x.Name.Remove(0, x.Name.IndexOf("@") + 1)))
        {
            if (!ImGui.TreeNode(character.Key)) continue;
            foreach (var currentChar in character)
            {
                var isSelected = index == selectedCharacterIndex;

                if (ImGui.Selectable(currentChar.Name, isSelected))
                {
                    overViewIsSelected = false;
                    selectedCharacterIndex = index;
                }

                index++;
            }

            ImGui.TreePop();
        }

        ImGui.EndChild();
    }

    private void DrawContent()
    {
        ImGui.SameLine();
        ImGui.BeginChild("RightChild", new Vector2(0, 0), true);

        if (overViewIsSelected)
        {
            OverviewWindow.Draw(CharacterContainers, _commandManager);
        }
        else
        {
            var temp = new List<CharacterContainer> { CharacterContainers[selectedCharacterIndex] };
            CurrencyWindow.Draw(temp);
            CollectionWindow.Draw(temp);
        }

        ImGui.EndChild();
    }
}