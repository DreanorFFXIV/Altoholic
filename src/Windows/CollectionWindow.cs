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
                DrawCollection(characterContainers, nameof(CharacterContainer.Collection), nameof(CharacterContainer.Collection.UnlockedMounts));
            }
            
            if (ImGui.TreeNode("Minons"))
            {
                DrawCollection(characterContainers, nameof(CharacterContainer.Collection), nameof(CharacterContainer.Collection.UnlockedMinions));
            }
            
            if (ImGui.TreeNode("Emotes"))
            {
                DrawCollection(characterContainers, nameof(CharacterContainer.Collection), nameof(CharacterContainer.Collection.UnlockedEmotes));
            }
            
            if (ImGui.TreeNode("Triple Triad Cards"))
            {
                DrawCollection(characterContainers, nameof(CharacterContainer.Collection), nameof(CharacterContainer.Collection.UnlockedTriadCards));
            }
            
            if (ImGui.TreeNode("Orchestrion Rolls"))
            {
                DrawCollection(characterContainers, nameof(CharacterContainer.Collection), nameof(CharacterContainer.Collection.UnlockedOrechestrion));
            }
            
            ImGui.TreePop();
        }
    }

    private static void DrawCollection(List<CharacterContainer> characterContainers, string mainProp, string subProp)
    {
        List<string> masterlist = new List<string>();

        int count = characterContainers.Count + 1;
        if (ImGui.BeginTable("table", count,
                ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
        {
            ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthFixed);
            foreach (var characterContainer in characterContainers)
            {
                object collection = characterContainer.GetType().GetProperty(mainProp).GetValue(characterContainer);
                dynamic resolvedUnlockedProperty = collection.GetType().GetProperty(subProp).GetValue(collection);

                ImGui.TableSetupColumn(characterContainer.Name, ImGuiTableColumnFlags.WidthFixed);
                masterlist.AddRange(resolvedUnlockedProperty);
            }

            ImGui.TableHeadersRow();
            ImGui.TableNextRow();
        }

        masterlist = masterlist.Distinct().Order().ToList();

        foreach (var item in masterlist)
        {
            ImGui.TableNextColumn();
            ImGui.Text(item);
 
            foreach (var characterContainer in characterContainers)
            {
                object collection = characterContainer.GetType().GetProperty(mainProp).GetValue(characterContainer);
                dynamic resolvedUnlockedProperty = collection.GetType().GetProperty(subProp).GetValue(collection);

                ImGui.TableNextColumn();
                if (resolvedUnlockedProperty.Contains(item))
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
}