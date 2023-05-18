using Newtonsoft.Json;

namespace Altoholic.Data.Currency;

public class Currencies
{
    public CommonCurrency Common { get; }
    public BattleCurrency Battle { get; }
    public OtherCurrency Other { get; }
    public TribalCurrency Tribal { get; }

    [JsonConstructor]
    public Currencies(CommonCurrency common, BattleCurrency battle, OtherCurrency other, TribalCurrency tribal)
    {
        Common = common;
        Battle = battle;
        Other = other;
        Tribal = tribal;
    }
     
    public Currencies()
    {
        Common = new CommonCurrency();
        Battle = new BattleCurrency();
        Other = new OtherCurrency();
        Tribal = new TribalCurrency();
        Refresh();
    }

    public void Refresh()
    {
        Common.Refresh();
        Battle.Refresh();
        Other.Refresh();
        Tribal.Refresh();
    }
}
