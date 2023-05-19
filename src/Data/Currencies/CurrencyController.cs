using Newtonsoft.Json;

namespace Altoholic.Data.Currencies;

public class CurrencyController
{
    public CommonCurrency Common { get; }
    public BattleCurrency Battle { get; }
    public OtherCurrency Other { get; }
    public TribalCurrency Tribal { get; }

    [JsonConstructor]
    public CurrencyController(CommonCurrency common, BattleCurrency battle, OtherCurrency other, TribalCurrency tribal)
    {
        Common = common;
        Battle = battle;
        Other = other;
        Tribal = tribal;
    }
     
    public CurrencyController()
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
