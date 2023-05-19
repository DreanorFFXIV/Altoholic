using System.Collections.Generic;
using System.Linq;
using Altoholic.Data;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace Altoholic.Windows;

public class CollectionWindow : BaseWindow
{
    public static void Draw(List<CharacterContainer> characterContainers)
    {
        if (ImGui.TreeNode("Collections"))
        {
            if (ImGui.TreeNode("Mounts"))
            {
                List<string> masterlist = new List<string>();

                int count = characterContainers.Count + 1;
                if (ImGui.BeginTable("table", count, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
                {
                    ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthFixed);
                    foreach (var characterContainer in characterContainers)
                    {
                        ImGui.TableSetupColumn(characterContainer.Name, ImGuiTableColumnFlags.WidthFixed);
                        masterlist.AddRange(characterContainer.Collection.UnlockedMounts);
                    }

                    ImGui.TableHeadersRow();
                    ImGui.TableNextRow();
                }
                
                masterlist = masterlist.Distinct().Order().ToList();
                
                foreach (var mount in masterlist)
                {
                    ImGui.TableNextColumn();
                    ImGui.Text(mount);
                        
                    foreach (var characterContainer in characterContainers)
                    {
                        ImGui.TableNextColumn();
                        if (characterContainer.Collection.UnlockedMounts.Contains(mount))
                        {
                            ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), "Unlocked");
                        }
                        else
                        {
                            ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), "Locked");  
                        }
                    }
                    ImGui.TableNextRow(); 
                }
               
                ImGui.EndTable();

                ImGui.TreePop();
            }
            
            if (ImGui.TreeNode("Minons"))
            {
                List<string> masterlist = new List<string>();

                int count = characterContainers.Count + 1;
                if (ImGui.BeginTable("table", count, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
                {
                    ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthFixed);
                    foreach (var characterContainer in characterContainers)
                    {
                        ImGui.TableSetupColumn(characterContainer.Name, ImGuiTableColumnFlags.WidthFixed);
                        masterlist.AddRange(characterContainer.Collection.UnlockedMinions);
                    }

                    ImGui.TableHeadersRow();
                    ImGui.TableNextRow();
                }
                
                masterlist = masterlist.Distinct().Order().ToList();
                
                foreach (var minion in masterlist)
                {
                    ImGui.TableNextColumn();
                    ImGui.Text(minion);
                        
                    foreach (var characterContainer in characterContainers)
                    {
                        ImGui.TableNextColumn();
                        if (characterContainer.Collection.UnlockedMinions.Contains(minion))
                        {
                            ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), "Unlocked");
                        }
                        else
                        {
                            ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), "Locked");  
                        }
                    }
                    ImGui.TableNextRow(); 
                }
               
                ImGui.EndTable();

                ImGui.TreePop();
            }
            
            ImGui.TreePop();
        }
    }
}