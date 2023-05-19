using System.Collections.Generic;
using System.Linq;
using Dalamud.Data;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;
using Newtonsoft.Json;

namespace Altoholic.Data.Collections;

public class CollectionController
{
    private readonly DataManager _dataManager;

    public List<string> UnlockedMounts { get; private set; } = new List<string>();
    public List<string> UnlockedMinions { get; private set; } = new List<string>();
    
    [JsonConstructor]
    public CollectionController()
    {
    }
 
    public CollectionController(DataManager dataManager)
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
        }
    }
}