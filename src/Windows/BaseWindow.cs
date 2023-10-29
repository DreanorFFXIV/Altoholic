using System.Collections.Generic;
using System.Reflection;
using Altoholic.Data;
using ImGuiNET;

namespace Altoholic.Windows;

public abstract class BaseWindow
{
    protected static void DrawRowsHorizontal<T>(List<CharacterContainer> characterContainers, string mainProp, string subProp)
    {
        DrawTableHorizontal<T>();
        foreach (var character in characterContainers)
        {
            ImGui.TableNextColumn();
            ImGui.Text(character.Name);
 
            var prop = character.GetType().GetProperty(mainProp);
            object? obj = string.IsNullOrWhiteSpace(subProp) 
                ? prop?.GetValue(character, null) 
                : prop?.GetValue(character, null)?.GetType().GetProperty(subProp)?.GetValue(prop.GetValue(character, null), null);

            DrawRows<T>(obj);
        }
        ImGui.EndTable();
    }

    protected static void DrawRowsVertical<T>(List<CharacterContainer> characterContainers, string mainProp, string subProp)
    {
        DrawTableVertical(characterContainers);
        int count = 0;
        foreach (var p in typeof(T).GetProperties())
        {
            ImGui.TableNextColumn();
            ImGui.Text(p.Name);
 
            foreach (var character in characterContainers)
            {
                var prop = character.GetType().GetProperty(mainProp);
                object? obj = string.IsNullOrWhiteSpace(subProp) 
                    ? prop?.GetValue(character, null) 
                    : prop?.GetValue(character, null)?.GetType().GetProperty(subProp)?.GetValue(prop.GetValue(character, null), null);


                ImGui.TableNextColumn();
                ImGui.Text( typeof(T).GetProperties()[count].GetValue(obj).ToString()); 
            }

            count++;
            ImGui.TableNextRow(); 
        }
        ImGui.EndTable();
    }

    protected static void DrawRows<T>(object obj)
    {
        foreach (PropertyInfo p in typeof(T).GetProperties())
        {
            ImGui.TableNextColumn();
            ImGui.Text(p.GetValue(obj).ToString()); 
        }
        ImGui.TableNextRow();
    }
  
    protected static void DrawTableHorizontal<T>(string firstColumn = "Character Name")
    {
        int size = typeof(T).GetProperties().Length + 1;
        if (ImGui.BeginTable("table", size, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
        {
            ImGui.TableSetupColumn(firstColumn, ImGuiTableColumnFlags.WidthFixed);

            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                ImGui.TableSetupColumn(p.Name, ImGuiTableColumnFlags.WidthFixed);
            }
            ImGui.TableHeadersRow();
            ImGui.TableNextRow();
        }
    }

    protected static void DrawTableVertical(List<CharacterContainer> characterContainers, bool enableHorizontalScrolling = false)
    {
        int size = characterContainers.Count + 1;

        var flags = ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner;
        if (enableHorizontalScrolling)
        {
            flags |= ImGuiTableFlags.ScrollX;
        }
        
        if (ImGui.BeginTable("table", size, flags))
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