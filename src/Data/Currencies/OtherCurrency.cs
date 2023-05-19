using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currencies;

public class OtherCurrency
{
    public string WhiteCrafterScrips { get; private set; } = string.Empty;
    public string PurpleCrafterScrips { get; private set; } = string.Empty;
    public string WhiteGathererScrips { get; private set; } = string.Empty;
    public string PurpleGathererScrips { get; private set; } = string.Empty;
    public string SkybuilderScrips { get; private set; } = string.Empty;

    public OtherCurrency()
    {
    }

    public void Refresh()
    {
        unsafe
        {
            WhiteCrafterScrips = InventoryManager.Instance()->GetInventoryItemCount(025199).ToString("N0");
            PurpleCrafterScrips = InventoryManager.Instance()->GetInventoryItemCount(033913).ToString("N0");
            WhiteGathererScrips = InventoryManager.Instance()->GetInventoryItemCount(025200).ToString("N0");
            PurpleGathererScrips = InventoryManager.Instance()->GetInventoryItemCount(033914).ToString("N0");
            SkybuilderScrips = InventoryManager.Instance()->GetInventoryItemCount(028063).ToString("N0");
        }
    }
}