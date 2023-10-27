using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currencies;

public class BattleCurrency
{
    public string Poetics { get; set; } = string.Empty;
    public string Astronomy { get; set; } = string.Empty;
    public string Causality { get; set; } = string.Empty;
    public string WeeklyCap { get; set; } = string.Empty;
    public string WolfMark { get; set; } = string.Empty;
    public string TrophyCrystal { get; set; } = string.Empty;
    public string Allied { get; set; } = string.Empty;
    public string Centurio { get; set; } = string.Empty;
    public string Nuts { get; set; } = string.Empty;
    public string Gemstone { get; set; } = string.Empty;

    public BattleCurrency()
    {
    }

    public void Refresh()
    {
        unsafe
        {
            WeeklyCap = $"{InventoryManager.Instance()->GetWeeklyAcquiredTomestoneCount()}/{InventoryManager.GetLimitedTomestoneWeeklyLimit()}";
            Poetics = InventoryManager.Instance()->GetInventoryItemCount(000028).ToString("N0");
            Astronomy = InventoryManager.Instance()->GetInventoryItemCount(000043).ToString("N0");
            Causality = InventoryManager.Instance()->GetInventoryItemCount(000044).ToString("N0");
            WolfMark = InventoryManager.Instance()->GetInventoryItemCount(000025).ToString("N0");
            TrophyCrystal = InventoryManager.Instance()->GetInventoryItemCount(036656).ToString("N0");
            Allied = InventoryManager.Instance()->GetInventoryItemCount(000027).ToString("N0");
            Centurio = InventoryManager.Instance()->GetInventoryItemCount(010307).ToString("N0");
            Nuts = InventoryManager.Instance()->GetInventoryItemCount(026533).ToString("N0");
            Gemstone = InventoryManager.Instance()->GetInventoryItemCount(026807).ToString("N0");
        }
    }
}