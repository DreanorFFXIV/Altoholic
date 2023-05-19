using Altoholic.Data.Character;
using Altoholic.Data.Currency;
using Altoholic.Windows;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class CharacterContainer
{
    public string Name { get; }
    
    public Currencies Currencies { get; }
    public Overview Overview { get; }

    [JsonConstructor]
    public CharacterContainer(string name, Currencies currencies, Overview overview)
    {
        Name = name;
        Currencies = currencies;
        Overview = overview;
        Overview.Gil = currencies.Common.Gil;
    }

    public CharacterContainer(string name)
    {
        Name = name;
        Currencies = new Currencies();
        Overview = new Overview { Gil = Currencies.Common.Gil };
        Reload();
    }

    public void Reload()
    {
        Currencies.Refresh();
        Overview.Refresh();
    }
}