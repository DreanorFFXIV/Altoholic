using System.Collections.Generic;
using Altoholic.Data;
using Dalamud.Game.Command;
using ImGuiNET;

namespace Altoholic.Windows;

public class OverviewWindow : BaseWindow
{
    public static void Draw(List<CharacterContainer> characterContainers, CommandManager commandManager)
    {
        if (ImGui.TreeNode("Overview"))
        {
            DrawTableHorizontal<Overview>("Relog");
            foreach (var character in characterContainers)
            {
                ImGui.TableNextColumn();
                if (ImGui.Button($"{character.Name}"))
                {
                    commandManager.ProcessCommand($"/autoretainer relog {character.Name}");
                }

                DrawRows<Overview>(character.Overview);
            }
            ImGui.EndTable();
            ImGui.TreePop();
        }
    }
}