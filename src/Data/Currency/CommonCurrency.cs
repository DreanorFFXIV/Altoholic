using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currency;

public class CommonCurrency
{
    private const uint _gilId = 000001;
    private const uint _stormId = 000020;
    private const uint _serpentId = 000021;
    private const uint _flameId = 000022;
    private const uint _mgp = 000029;
    private const uint _ventures = 021072;
    
    public int Gil { get; set; }

    public int StormSeals { get; set; }
    
    public int SerpentSeals { get; set; }

    public int FlameSeals { get; set; }

    public int Mgp { get; set; }

    public int Ventures { get; set; }
    
    public CommonCurrency()
    {
    }
    
    public void Refresh()
    {
        unsafe
        {
            Gil = InventoryManager.Instance()->GetInventoryItemCount(_gilId);
            StormSeals = InventoryManager.Instance()->GetInventoryItemCount(_stormId);
            SerpentSeals = InventoryManager.Instance()->GetInventoryItemCount(_serpentId);
            FlameSeals = InventoryManager.Instance()->GetInventoryItemCount(_flameId);
            Ventures = InventoryManager.Instance()->GetInventoryItemCount(_ventures);
            Mgp = InventoryManager.Instance()->GetInventoryItemCount(_mgp);
        }
    }
}