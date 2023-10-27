using FFXIVClientStructs.FFXIV.Client.Game;

namespace Altoholic.Data.Currencies;

public class TribalCurrency
{
    public string Amaljaa { get; set; } = string.Empty;
    public string Sylph { get; set; } = string.Empty;
    public string Kobold { get; set; } = string.Empty;
    public string Sahagin { get; set; } = string.Empty;
    public string Ixali { get; set; } = string.Empty;
    public string Vanu { get; set; } = string.Empty;
    public string Vath { get; set; } = string.Empty;
    public string Moogle { get; set; } = string.Empty;
    public string Kojin { get; set; } = string.Empty;
    public string Ananta { get; set; } = string.Empty;
    public string Namazu { get; set; } = string.Empty;
    public string Pixie { get; set; } = string.Empty;
    public string Qitari { get; set; } = string.Empty;
    public string Dwarf { get; set; } = string.Empty;
    public string Arkasodara { get; set; } = string.Empty;
    public string Omicron { get; set; } = string.Empty;
    public string Loporrrit { get; set; } = string.Empty;

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