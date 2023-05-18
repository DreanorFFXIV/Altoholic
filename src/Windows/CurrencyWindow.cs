using System.Collections.Generic;
using System.Reflection;
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
                DrawRowsVertical<CommonCurrency>(characterContainers, nameof(CharacterContainer.Currencies), nameof(CharacterContainer.Currencies.Common));
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Battle"))
            {
                DrawRowsVertical<BattleCurrency>(characterContainers, nameof(CharacterContainer.Currencies), nameof(CharacterContainer.Currencies.Battle));
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Other"))
            {
                DrawRowsVertical<OtherCurrency>(characterContainers, nameof(CharacterContainer.Currencies), nameof(CharacterContainer.Currencies.Other));
                ImGui.TreePop();
            }
 
            if (ImGui.TreeNode("Tribal"))
            {
                DrawRowsVertical<TribalCurrency>(characterContainers, nameof(CharacterContainer.Currencies), nameof(CharacterContainer.Currencies.Tribal));
                ImGui.TreePop();
            }
   
            ImGui.TreePop();
        }
    }
    
    private static void DrawRowsHorizontal<T>(List<CharacterContainer> characterContainers, string mainProp, string subProp)
    {
        DrawTableHorizontal<T>();
        foreach (var character in characterContainers)
        {
            ImGui.TableNextColumn();
            ImGui.Text(character.Name);
 
            var prop = character.GetType().GetProperty(mainProp);
            var obj = prop?.GetValue(character, null)
                ?.GetType().GetProperty(subProp)?.GetValue(prop.GetValue(character, null), null);

            DrawRows<T>(obj);
        }
        ImGui.EndTable();
    }
    
    private static void DrawRowsVertical<T>(List<CharacterContainer> characterContainers, string mainProp, string subProp)
    {
        DrawTableVertical<T>(characterContainers);
        int count = 0;
        foreach (var p in typeof(T).GetProperties())
        {
            ImGui.TableNextColumn();
            ImGui.Text(p.Name);
 
            foreach (var character in characterContainers)
            {
                var prop = character.GetType().GetProperty(mainProp);
                var obj = prop?.GetValue(character, null)
                    ?.GetType().GetProperty(subProp)?.GetValue(prop.GetValue(character, null), null);

                ImGui.TableNextColumn();
                ImGui.Text( typeof(T).GetProperties()[count].GetValue(obj).ToString()); 
            }

            count++;
            ImGui.TableNextRow(); 
        }
        ImGui.EndTable();
    }
}