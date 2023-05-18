using Altoholic.Data.Currency;
using FFXIVClientStructs.FFXIV.Client.Game;
using Newtonsoft.Json;

namespace Altoholic.Data;

public class Currencies
{
    public CommonCurrency Common { get; private set; }
    public BattleCurrency Battle { get; private set; }

    [JsonConstructor]
    public Currencies(CommonCurrency common, BattleCurrency battle)
    {
        Common = common;
        Battle = battle;
    }
     
    public Currencies()
    {
        Common = new CommonCurrency();
        Battle = new BattleCurrency();
        Refresh();
    }

    public void Refresh()
    {
        Common.Refresh();
        Battle.Refresh();
    }
}
