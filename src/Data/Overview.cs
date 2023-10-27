using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace Altoholic.Data;

public class Overview
{
    public string Gil { get; set; } = string.Empty;

    public string Commendations { get; set; } = string.Empty;

    //ToDo: Add Free Company Credits
    //ToDo: Add Statistics for all Collections x/x Mounts, Minions, Emotes, Triple Triad Cards, Orchestrion Rolls
    public string WeeklyCap { get; set; } = string.Empty;
    
    public Overview()
    {
    }
    
    public void Refresh()
    {
        unsafe 
        {
            Gil = InventoryManager.Instance()->GetInventoryItemCount(000001).ToString("N0");
            WeeklyCap = $"{InventoryManager.Instance()->GetWeeklyAcquiredTomestoneCount()}/{InventoryManager.GetLimitedTomestoneWeeklyLimit()}";
            Commendations = PlayerState.Instance()->PlayerCommendations.ToString("N0");
        }
    }
}