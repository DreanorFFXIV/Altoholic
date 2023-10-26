using Altoholic.Data.Currencies;
using Dalamud.Data;
using Dalamud.Plugin.Services;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class CharacterContainer
{
    public string Name { get; }
    
    public CurrencyController Currency { get; }
    public Overview Overview { get; }
    public CollectionContainer Collection { get; private set; }
    public JobContainer JobContainer { get; private set; }

    [JsonConstructor]
    public CharacterContainer(string name, CurrencyController currency, Overview overview, CollectionContainer collection, JobContainer jobContainer)
    {
        Name = name;
        Currency = currency;
        Overview = overview;
        Overview.Gil = currency.Common.Gil;
        Collection = collection;
        JobContainer = jobContainer;
    }
 
    public CharacterContainer(string name, IDataManager dataManager)
    {
        Name = name;
        Currency = new CurrencyController();
        Overview = new Overview { Gil = Currency.Common.Gil };

        Reload(dataManager); 
    }

    public void Reload(IDataManager dataManager)
    {
        Collection = new CollectionContainer(dataManager);
        JobContainer = new JobContainer(dataManager);
        Currency.Refresh();
        Overview.Refresh();
        Collection.Refresh();
        JobContainer.Refresh();
    }
}