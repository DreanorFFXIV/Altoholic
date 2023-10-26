using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace Altoholic.Data;

public class Overview
{
    public string Gil { get; set; } = string.Empty;

    public string Commendations { get; set; } = string.Empty;

    public string Mounts { get; set; } = string.Empty;
    
    public Overview()
    {
    }
    
    public void Refresh()
    {
        unsafe 
        {
            Gil = InventoryManager.Instance()->GetInventoryItemCount(000001).ToString("N0");
            Mounts = PlayerState.Instance()->NumOwnedMounts.ToString();
            Commendations = PlayerState.Instance()->PlayerCommendations.ToString("N0");
        }
    }
}