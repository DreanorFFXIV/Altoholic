using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace Altoholic.Windows;

public class MainWindow : Window, IDisposable
{
    public List<CharacterData> CharacterData { get; set; }

    public MainWindow() 
        : base("Altoholic", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
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
        ImGui.Spacing();
 
        if (ImGui.BeginTable("table", 6, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.BordersInner))
        {
            ImGui.TableHeader("Alts");
            ImGui.TableSetupColumn("Character Name", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn("Poetics", ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn("Astronomy", ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn("Causality", ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn("Weekly Tomes", ImGuiTableColumnFlags.WidthFixed);
            ImGui.TableSetupColumn("Gil", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableHeadersRow();
            ImGui.TableNextRow();

            foreach (var character in CharacterData)
            {
                DrawRow(character);
            }

            DrawTotal();

            ImGui.EndTable();
        }
    }

    private void DrawTotal()
    {
        ImGui.TableNextRow();

        ImGui.TableNextColumn();
        ImGui.Text("Total:");

        ImGui.TableNextColumn();
        ImGui.Text(CharacterData.Sum(x => x.Poetics).ToString());

        ImGui.TableNextColumn();
        ImGui.Text(CharacterData.Sum(x => x.NormalTomes).ToString());

        ImGui.TableNextColumn();
        ImGui.Text(CharacterData.Sum(x => x.CurrentTomes).ToString());

        ImGui.TableNextColumn();
        ImGui.Text("-");

        ImGui.TableNextColumn();
        ImGui.Text(CharacterData.Sum(x => x.Gil).ToString("#,##0.00"));
    }

    private static void DrawRow(CharacterData character)
    {
        ImGui.TableNextColumn();
        ImGui.Text(character.CharacterName);

        ImGui.TableNextColumn();
        ImGui.Text(character.Poetics.ToString());

        ImGui.TableNextColumn();
        ImGui.Text(character.NormalTomes.ToString());
        
        ImGui.TableNextColumn();
        ImGui.Text(character.CurrentTomes.ToString());

        ImGui.TableNextColumn();
        ImGui.Text($"{character.WeeklyTomes}/{character.TomeCap}");

        ImGui.TableNextColumn();
        ImGui.Text(character.Gil.ToString("#,##0.00"));

        ImGui.TableNextRow();
    }
}
