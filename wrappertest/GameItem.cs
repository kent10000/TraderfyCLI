using Traderfy.TarkovSharp;

namespace wrappertest;

public class GameItem
{
    private readonly string _itemName;
    private readonly long _fleaMarketPrice;
    private readonly TraderSellPrice[] _traderSellPrices;

    public GameItem(string itemName, long fleaMarketPrice, List<TraderSellPrice> traderSellPrices)
    {
        _itemName = itemName;
        _fleaMarketPrice = fleaMarketPrice;
        _traderSellPrices = traderSellPrices.ToArray();
    }
    
    public string Name => _itemName;
    public long FleaMarketPrice => _fleaMarketPrice;

    public long GetIncomeFrom(TraderName trader)
    {
        if (_fleaMarketPrice == 0) return 0;
        return GetPriceFor(trader) - _fleaMarketPrice;
    }
    public long GetPriceFor(TraderName trader)
    {
        try
        {
            return (from t in _traderSellPrices where t.Trader == trader select t.Price).First();
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
        
    }
}

public struct TraderSellPrice
{
    private long _price;
    private TraderName _trader;
    public TraderSellPrice(long price, string trader)
    {
        _trader = new TraderName();
        _trader = trader switch
        {
            "Prapor" => TraderName.prapor,
            "Therapist" => TraderName.therapist,
            "Fence" => TraderName.fence,
            "Mechanic" => TraderName.mechanic,
            "Skier" => TraderName.skier,
            "Peacekeeper" => TraderName.peacekeeper,
            "Jaeger" => TraderName.jaeger,
            "Ragman" => TraderName.ragman,
            _ => throw new Exception("Unknown trader")
        };
        _price = price;
    }
    
    public long  Price => _price;
    public TraderName Trader => _trader;
}