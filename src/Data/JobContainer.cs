using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Data;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Common.Math;
using Lumina.Excel.GeneratedSheets;

namespace Altoholic.Data;

public class JobContainer
{
    private DataManager _dataManager;

    public List<Job> Jobs { get; private set; } = new List<Job>();

    private List<uint> _dps = new List<uint>{22, 23, 25, 27, 30, 31, 20, 34, 35, 36, 38, 39};
    private List<uint> _heal = new List<uint>{24, 28, 33, 40};
    private List<uint> _tank = new List<uint>{19, 21, 32, 37};
    private List<uint> _gather = new List<uint>{16, 17, 18};
    private List<uint> _craft = new List<uint>{8, 9, 10, 11, 12, 13, 14, 15};

    public JobContainer(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public void Refresh()
    {
        if (_dataManager == null)
        {
            return;
        } 

        Jobs = new List<Job>();

        List<uint> displayedJobs = new List<uint>();
        displayedJobs.AddRange(_dps);
        displayedJobs.AddRange(_heal);
        displayedJobs.AddRange(_tank);
        displayedJobs.AddRange(_gather);
        displayedJobs.AddRange(_craft);
        
        unsafe 
        {
            var a = _dataManager.GetExcelSheet<ClassJob>().Select(x => new {x.ExpArrayIndex, x.Abbreviation, x.RowId}).ToList();

            for (var i = 0; i < 30; i++)
            {
                var jobs = a.Where(x => x.ExpArrayIndex == i);
                foreach (var job in jobs)
                {
                    if (!displayedJobs.Contains(job.RowId)) { continue; }
                    
                    Vector4 color = new Vector4();
                    int sort = 0;
                    if (_tank.Contains(job.RowId))
                    {
                        color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
                        sort = 1;
                    }

                    if (_dps.Contains(job.RowId))
                    {
                        color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                        sort = 2;
                    }

                    if (_heal.Contains(job.RowId))
                    {
                        color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                        sort = 3;
                    }

                    if (_craft.Contains(job.RowId))
                    {
                        color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
                        sort = 4;
                    }
                    
                    if (_gather.Contains(job.RowId))
                    {
                        color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
                        sort = 5;
                    }

                    Jobs.Add(new Job(job.Abbreviation, PlayerState.Instance()->ClassJobLevelArray[i], color, sort));
                }
            }
            
            Jobs = Jobs.OrderBy(x => x.Sort).ToList();
        }
    }
}