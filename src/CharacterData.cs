using FFXIVClientStructs.FFXIV.Client.Game;
using Newtonsoft.Json;

namespace Altoholic;

public class CharacterData
{
    private const uint _astroId = 000043;
    private const uint _causalId = 000044;
    private const uint _poeticId = 000028;
    private const uint _gilId = 000001;
        
    public string CharacterName { get; private set; }
    public int TomeCap { get; private set; }
    public int WeeklyTomes { get; private set; }
    public int CurrentTomes { get; private set; }
    public int NormalTomes { get; private set; }
    public int Poetics { get; private set; }
    public int Gil { get; private set; }

    [JsonConstructor]
    public CharacterData(string characterName, int tomeCap, int weeklyTomes, int currentTomes, int normalTomes, int poetics, int gil)
    {
        CharacterName = characterName ?? "ERROR";
        TomeCap = tomeCap;
        WeeklyTomes = weeklyTomes;
        CurrentTomes = currentTomes;
        NormalTomes = normalTomes;
        Poetics = poetics;
        Gil = gil;
    }
     
    public CharacterData(string characterName)
    {
        CharacterName = characterName;
        TomeCap = InventoryManager.GetLimitedTomestoneWeeklyLimit();
        Refresh();
    }

    public void Refresh()
    {
        unsafe
        {
            WeeklyTomes = InventoryManager.Instance()->GetWeeklyAcquiredTomestoneCount();
            NormalTomes = InventoryManager.Instance()->GetInventoryItemCount(_astroId);
            CurrentTomes = InventoryManager.Instance()->GetInventoryItemCount(_causalId);
            Poetics = InventoryManager.Instance()->GetInventoryItemCount(_poeticId);
            Gil = InventoryManager.Instance()->GetInventoryItemCount(_gilId);
        }
    }
}
