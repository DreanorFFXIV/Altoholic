using System.Collections.Generic;
using System.Linq;
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
            
            ImGui.TreePop();
        }
    }
}