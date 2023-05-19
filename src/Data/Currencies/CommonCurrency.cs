using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currencies;

public class CommonCurrency
{
    public string Gil { get; set; } = string.Empty;

    public string StormSeals { get; set; } = string.Empty;
    
    public string SerpentSeals { get; set; } = string.Empty;

    public string FlameSeals { get; set; } = string.Empty;

    public string Mgp { get; set; } = string.Empty;

    public string Ventures { get; set; } = string.Empty;
    
    public CommonCurrency()
    {
    }
    
    public void Refresh()
    {
        unsafe
        {
            Gil = InventoryManager.Instance()->GetInventoryItemCount(000001).ToString("N0");
            StormSeals = InventoryManager.Instance()->GetInventoryItemCount(000020).ToString("N0");
            SerpentSeals = InventoryManager.Instance()->GetInventoryItemCount(000021).ToString("N0");
            FlameSeals = InventoryManager.Instance()->GetInventoryItemCount(000022).ToString("N0");
            Ventures = InventoryManager.Instance()->GetInventoryItemCount(021072).ToString("N0");
            Mgp = InventoryManager.Instance()->GetInventoryItemCount(000029).ToString("N0");
        }
    }
}