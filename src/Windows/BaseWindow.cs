using System.Collections.Generic;
using System.Reflection;
using Altoholic.Data;
using Dalamud.Logging;
using ImGuiNET;

namespace Altoholic.Windows;

public abstract class BaseWindow
{
    public static void DrawRows<T>(object obj)
    {
        foreach (PropertyInfo p in typeof(T).GetProperties())
        {
            ImGui.TableNextColumn();
            ImGui.Text(p.GetValue(obj).ToString()); 
        }
        ImGui.TableNextRow();
    }

    public static void DrawTableHorizontal<T>()
    {
        int size = typeof(T).GetProperties().Length + 1;
        if (ImGui.BeginTable("table", size, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
        {
            ImGui.TableSetupColumn("Character Name", ImGuiTableColumnFlags.WidthFixed);

            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                ImGui.TableSetupColumn(p.Name, ImGuiTableColumnFlags.WidthFixed);
            }
            ImGui.TableHeadersRow();
            ImGui.TableNextRow();
        }
    }
    
    public static void DrawTableVertical<T>(List<CharacterContainer> characterContainers)
    {
        int size = characterContainers.Count + 1;
        if (ImGui.BeginTable("table", size, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
        {
            ImGui.TableSetupColumn("Name", ImGuiTableColumnFlags.WidthFixed);

            foreach (var p in characterContainers)
            {
                ImGui.TableSetupColumn(p.Name, ImGuiTableColumnFlags.WidthFixed);
            }
            ImGui.TableHeadersRow();
            ImGui.TableNextRow();
        }
    }
}