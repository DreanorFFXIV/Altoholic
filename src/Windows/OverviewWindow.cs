using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Altoholic.Data;
using Dalamud.Game.Command;
using Dalamud.Logging;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace Altoholic.Windows;

public class OverviewWindow : BaseWindow
{
    public static void Draw(List<CharacterContainer> characterContainers, ICommandManager commandManager)
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

        DrawSummaryLine(characterContainers);

        if (ImGui.TreeNode("Jobs"))
        {
            DrawTableVertical(characterContainers);
            int count = 0;
            foreach (var p in characterContainers.First().JobContainer.Jobs)
            {
                bool first=true;
                foreach (var character in characterContainers)
                {
                    var job = character.JobContainer.Jobs[count];

                    if (first)
                    {
                        ImGui.TableNextColumn();
                        ImGui.TextColored(job.Color, p.Name);
                        first = false;
                    }
                    
                    ImGui.TableNextColumn();
                    ImGui.TextColored(job.Color, job.Level.ToString());
                }

                count++;
                ImGui.TableNextRow(); 
            }
           
            ImGui.EndTable();
            ImGui.TreePop();
        }
    }

    private static void DrawSummaryLine(List<CharacterContainer> characterContainers)
    {
        ImGui.TableNextColumn();
        ImGui.Text("Total Gil:");
        ImGui.TableNextColumn();
        ImGui.Text(characterContainers.Sum(x => long.Parse(x.Currency.Common.Gil.Replace(",", "").Replace(".", ""))).ToString("N0"));
        ImGui.EndTable();
    }
}