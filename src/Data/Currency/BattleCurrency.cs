using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currency;

public class BattleCurrency
{
    private const uint _astroId = 000043;
    private const uint _causalId = 000044;
    private const uint _poeticId = 000028;
    private const uint _wolfMark = 000025;
    private const uint _trophy = 036656;
    private const uint _allied = 000027;
    private const uint _centurio = 010307;
    private const uint _nuts = 026533;
    
    public int Poetics { get; private set; }
    public int Astronomy { get; private set; }
    public int Causality { get; private set; }
    public string WeeklyCap { get; private set; }
    public int WolfMark { get; private set; }
    public int TrophyCrystal { get; private set; }
    public int Allied { get; private set; }
    public int Centurio { get; private set; }
    public int Nuts { get; private set; }
    
    public BattleCurrency()
    {
        WeeklyCap = string.Empty;
    }

    public void Refresh()
    {
        unsafe
        {
            WeeklyCap = $"{InventoryManager.Instance()->GetWeeklyAcquiredTomestoneCount()}/{InventoryManager.GetLimitedTomestoneWeeklyLimit()}";
            Poetics = InventoryManager.Instance()->GetInventoryItemCount(_poeticId);
            Astronomy = InventoryManager.Instance()->GetInventoryItemCount(_astroId);
            Causality = InventoryManager.Instance()->GetInventoryItemCount(_causalId);
            WolfMark = InventoryManager.Instance()->GetInventoryItemCount(_wolfMark);
            TrophyCrystal = InventoryManager.Instance()->GetInventoryItemCount(_trophy);
            Allied = InventoryManager.Instance()->GetInventoryItemCount(_allied);
            Centurio = InventoryManager.Instance()->GetInventoryItemCount(_centurio);
            Nuts = InventoryManager.Instance()->GetInventoryItemCount(_nuts);
        }
    }
}