using System.Collections.Generic;
using System.Linq;
using Altoholic.Data;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace Altoholic.Windows;

public abstract class OverviewWindow : BaseWindow
{
    private const float LightColor = 0.2f;
    private const float FullColor = 1.0f;
    
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
            DrawTableVertical(characterContainers, true);
            var count = 0;
            foreach (var p in characterContainers.First().JobContainer.Jobs)
            {
                var first=true;
                foreach (var job in characterContainers.Select(character => character.JobContainer.Jobs[count]))
                {
                    if (first)
                    {
                        ImGui.TableNextColumn();
                        ImGui.TextColored(GetJobColor(count), p.Name);
                        first = false;
                    }
                    
                    ImGui.TableNextColumn();
                    ImGui.TextColored(GetJobColor(count), job.Level.ToString());
                }

                count++;
                ImGui.TableNextRow(); 
            }
           
            ImGui.EndTable();
            ImGui.TreePop();
        }
    }
    
    private static Vector4 GetJobColor(int count)
    {
        int currentStep = 0;
        
        if (count <= JobValues.Tank.Count -1)
        {
            return new Vector4(LightColor, LightColor, FullColor, FullColor);
        }
        
        currentStep += JobValues.Tank.Count;
        if (count <= JobValues.Dps.Count + currentStep -1)
        {
            return new Vector4(FullColor, LightColor, LightColor, FullColor);
        }
        
        currentStep += JobValues.Dps.Count;
        if (count <= JobValues.Heal.Count + currentStep -1)
        {
            return new Vector4(LightColor, FullColor, LightColor, FullColor);
        }
        
        currentStep += JobValues.Heal.Count;
        if (count <= JobValues.Craft.Count + currentStep -1)
        {
            return new Vector4(FullColor,FullColor,LightColor, FullColor);
        }
        
        return new Vector4(1, 1, 1, 1);
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