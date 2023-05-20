using System.Collections.Generic;
using System.Linq;
using Dalamud.Data;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class CollectionContainer
{
    private readonly DataManager _dataManager;

    public List<string> UnlockedMounts { get; private set; } = new List<string>();
    public List<string> UnlockedMinions { get; private set; } = new List<string>();
    public List<string> UnlockedEmotes { get; private set; } = new List<string>();
    public List<string> UnlockedTriadCards { get; private set; } = new List<string>();
    public List<string> UnlockedOrechestrion { get; private set; } = new List<string>();
    
    [JsonConstructor]
    public CollectionContainer()
    {
    }
 
    public CollectionContainer(DataManager dataManager)
    {
        _dataManager = dataManager;
        Refresh();
    }

    public void Refresh()
    {
        if (_dataManager == null)
        {
            return;
        }
        unsafe
        {
            UnlockedMounts = new List<string>();
            UnlockedMinions = new List<string>();
            UnlockedEmotes = new List<string>();
            UnlockedTriadCards = new List<string>();
            UnlockedOrechestrion = new List<string>();
            
            foreach (var mount in _dataManager.GetExcelSheet<Mount>().OrderBy(x => x.Singular.RawString))
            {
                if (PlayerState.Instance()->IsMountUnlocked(mount.RowId))
                {
                    UnlockedMounts.Add(mount.Singular.RawString);
                }
            }
            
            foreach (var minion in _dataManager.GetExcelSheet<Companion>().OrderBy(x => x.Singular.RawString))
            {
                if (UIState.Instance()->IsCompanionUnlocked(minion.RowId))
                {
                    UnlockedMinions.Add(minion.Singular.RawString);
                }
            }
            
            foreach (var emote in _dataManager.GetExcelSheet<Emote>())
            {
                if (UIState.Instance()->IsEmoteUnlocked((ushort)emote.RowId))
                {
                    if (emote.UnlockLink != 0)
                    {
                        UnlockedEmotes.Add(emote.Name);
                    }
                }
            }
            
            foreach (var triadCard in _dataManager.GetExcelSheet<TripleTriadCard>())
            {
                if (UIState.Instance()->IsTripleTriadCardUnlocked((ushort)triadCard.RowId))
                {
                    UnlockedTriadCards.Add(triadCard.Name);
                }
            }
            
            foreach (var orchestrion in _dataManager.GetExcelSheet<Orchestrion>())
            {
                if (PlayerState.Instance()->IsOrchestrionRollUnlocked((ushort)orchestrion.RowId))
                {
                    UnlockedOrechestrion.Add(orchestrion.Name);
                }
            }
        }
    }
}