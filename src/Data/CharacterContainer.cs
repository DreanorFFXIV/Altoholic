using Altoholic.Data.Currency;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class CharacterContainer
{
    public string Name { get; }
    
    public Currencies Currencies { get; }

    [JsonConstructor]
    public CharacterContainer(string name, Currencies currencies)
    {
        Name = name;
        Currencies = currencies;
    }

    public CharacterContainer(string name)
    {
        Name = name;
        Currencies = new Currencies();
        Reload();
    }

    public void Reload()
    {
        Currencies.Refresh();
    }
}