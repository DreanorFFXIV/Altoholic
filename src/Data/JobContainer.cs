using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;

namespace Altoholic.Data;

public class JobContainer
{
    private readonly IDataManager _dataManager;

    public List<Job> Jobs { get; private set; } = new();

    public JobContainer(IDataManager dataManager)
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
        displayedJobs.AddRange(JobValues.Dps);
        displayedJobs.AddRange(JobValues.Heal);
        displayedJobs.AddRange(JobValues.Tank);
        displayedJobs.AddRange(JobValues.Gather);
        displayedJobs.AddRange(JobValues.Craft);

        unsafe 
        {
            var a = _dataManager.GetExcelSheet<ClassJob>().Select(x => new {x.ExpArrayIndex, x.Abbreviation, x.RowId}).ToList();

            for (var i = 0; i < 30; i++)
            {
                var jobs = a.Where(x => x.ExpArrayIndex == i);
                foreach (var job in jobs)
                {
                    if (!displayedJobs.Contains(job.RowId)) { continue; }

                    var newJob = new Job(job.Abbreviation, PlayerState.Instance()->ClassJobLevelArray[i]);
                    newJob.Sort = JobValues.GetSortIndex(job.RowId);
                    Jobs.Add(newJob);
                }
            }

            Jobs = Jobs.OrderBy(x => x.Sort).ToList();
        }
    }
}