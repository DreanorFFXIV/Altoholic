using System.Collections.Generic;
using Altoholic.Data;
using Altoholic.Data.Currency;
using ImGuiNET;

namespace Altoholic.Windows;

public class CurrencyWindow : BaseWindow
{
    public static void Draw(List<CharacterContainer> characterContainers)
    {
        if (ImGui.TreeNode("Currencies"))
        {
            if (ImGui.TreeNode("Common"))
            {
                DrawCommon(characterContainers);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Battle"))
            {
                DrawBattle(characterContainers);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Other"))
            {
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Tribal"))
            {
                ImGui.TreePop();
            }

            ImGui.TreePop();
        }
    }

    private static void DrawBattle(List<CharacterContainer> characterContainers)
    {
        DrawTable<BattleCurrency>();
        foreach (var character in characterContainers)
        {
            ImGui.TableNextColumn();
            ImGui.Text(character.Name);

            DrawRows<BattleCurrency>(character.Currencies.Battle);
        }
        ImGui.EndTable();
    }

    private static void DrawCommon(List<CharacterContainer> characterContainers)
    {
        DrawTable<CommonCurrency>();
        foreach (var character in characterContainers)
        {
            ImGui.TableNextColumn();
            ImGui.Text(character.Name);

            DrawRows<CommonCurrency>(character.Currencies.Common);
        }
        ImGui.EndTable();
    }
}