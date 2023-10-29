using System.Collections.Generic;
using Altoholic.Data;
using Altoholic.Data.Currencies;
using ImGuiNET;

namespace Altoholic.Windows;

public abstract class CurrencyWindow : BaseWindow
{
    public static void Draw(List<CharacterContainer> characterContainers)
    {
        if (ImGui.TreeNode("Currencies"))
        {
            if (ImGui.TreeNode("Common"))
            {
                DrawRowsVertical<CommonCurrency>(characterContainers, nameof(CharacterContainer.Currency), nameof(CharacterContainer.Currency.Common));
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Battle"))
            {
                DrawRowsVertical<BattleCurrency>(characterContainers, nameof(CharacterContainer.Currency), nameof(CharacterContainer.Currency.Battle));
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Other"))
            {
                DrawRowsVertical<OtherCurrency>(characterContainers, nameof(CharacterContainer.Currency), nameof(CharacterContainer.Currency.Other));
                ImGui.TreePop();
            }
  
            if (ImGui.TreeNode("Tribal"))
            {
                DrawRowsVertical<TribalCurrency>(characterContainers, nameof(CharacterContainer.Currency), nameof(CharacterContainer.Currency.Tribal));
                ImGui.TreePop();
            }
   
            ImGui.TreePop();
        }
    }
}