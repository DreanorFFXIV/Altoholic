using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currencies;

public class TribalCurrency
{
    public string Amaljaa { get; private set; } = string.Empty;
    public string Sylph { get; private set; } = string.Empty;
    public string Kobold { get; private set; } = string.Empty;
    public string Sahagin { get; private set; } = string.Empty;
    public string Ixali { get; private set; } = string.Empty;
    public string Vanu { get; private set; } = string.Empty;
    public string Vath { get; private set; } = string.Empty;
    public string Moogle { get; private set; } = string.Empty;
    public string Kojin { get; private set; } = string.Empty;
    public string Ananta { get; private set; } = string.Empty;
    public string Namazu { get; private set; } = string.Empty;
    public string Pixie { get; private set; } = string.Empty;
    public string Qitari { get; private set; } = string.Empty;
    public string Dwarf { get; private set; } = string.Empty;
    public string Arkasodara { get; private set; } = string.Empty;
    public string Omicron { get; private set; } = string.Empty;
    public string Loporrrit { get; private set; } = string.Empty;

    public TribalCurrency()
    {
    }

    public void Refresh()
    {
        unsafe
        {
            Amaljaa = InventoryManager.Instance()->GetInventoryItemCount(021076).ToString("N0");
            Sylph = InventoryManager.Instance()->GetInventoryItemCount(021075).ToString("N0");
            Kobold = InventoryManager.Instance()->GetInventoryItemCount(021078).ToString("N0");
            Sahagin = InventoryManager.Instance()->GetInventoryItemCount(021077).ToString("N0");
            Ixali = InventoryManager.Instance()->GetInventoryItemCount(021073).ToString("N0");
            Vanu = InventoryManager.Instance()->GetInventoryItemCount(021074).ToString("N0");
            Vath = InventoryManager.Instance()->GetInventoryItemCount(021079).ToString("N0");
            Moogle = InventoryManager.Instance()->GetInventoryItemCount(021080).ToString("N0");
            Kojin = InventoryManager.Instance()->GetInventoryItemCount(021081).ToString("N0");
            Ananta = InventoryManager.Instance()->GetInventoryItemCount(021935).ToString("N0");
            Namazu = InventoryManager.Instance()->GetInventoryItemCount(022525).ToString("N0");
            Pixie = InventoryManager.Instance()->GetInventoryItemCount(028186).ToString("N0");
            Qitari = InventoryManager.Instance()->GetInventoryItemCount(028187).ToString("N0");
            Dwarf = InventoryManager.Instance()->GetInventoryItemCount(028188).ToString("N0");
            Arkasodara = InventoryManager.Instance()->GetInventoryItemCount(036657).ToString("N0");
            Omicron = InventoryManager.Instance()->GetInventoryItemCount(037854).ToString("N0");
            Loporrrit = InventoryManager.Instance()->GetInventoryItemCount(038952).ToString("N0");
        }
    }
}