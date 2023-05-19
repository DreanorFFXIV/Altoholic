using Altoholic.Data.Collections;
using Altoholic.Data.Currencies;
using Dalamud.Data;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class CharacterContainer
{
    public string Name { get; }
    
    public CurrencyController Currency { get; }
    public Overview Overview { get; }
    public CollectionController Collection { get; }

    [JsonConstructor]
    public CharacterContainer(string name, CurrencyController currency, Overview overview, CollectionController collection)
    {
        Name = name;
        Currency = currency;
        Overview = overview;
        Overview.Gil = currency.Common.Gil;
        Collection = collection;
    }

    public CharacterContainer(string name, DataManager dataManager)
    {
        Name = name;
        Currency = new CurrencyController();
        Overview = new Overview { Gil = Currency.Common.Gil };
        Collection = new CollectionController(dataManager);
        Reload(); 
    }

    public void Reload()
    {
        Currency.Refresh();
        Overview.Refresh();
        Collection.Refresh();
    }
}