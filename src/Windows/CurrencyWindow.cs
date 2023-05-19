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
}