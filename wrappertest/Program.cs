using Newtonsoft.Json;
using Traderfy.TarkovSharp;
using wrappertest;
using wrappertest.TarkovSharp;

//var client = new TarkovApiClient("traders", new[]{"cashOffers { item {name avg24hPrice} priceRUB }"});
var client = new TarkovApiClient("items", new[]{"name", "avg24hPrice","sellFor {vendor { name } priceRUB}"});
//var d = new TarkovApiClient("playerLevels", new[]{"level", "exp"});
string? res = null;
try
{
    res = await client.GetRawResponse();
}
catch (HttpRequestException)
{
    Console.WriteLine("Error: Unable to establish an internet connection.\nPlease ensure you are connected to the internet and try again.");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey(true);
    Environment.Exit(1);

}

var json = JsonConvert.DeserializeObject<TraderPrice>(res);

var items = (from item in json?.Items let traderPrices = (from x in item.SellFor where x.Vendor.Name != "Flea Market" let buyer = x.Vendor.Name let price = x.PriceRub select new TraderSellPrice(price, buyer)).ToList() let name = item.Name let avg24HPrice = item.Avg24HPrice select new GameItem(name, avg24HPrice, traderPrices));

Console.WriteLine("Welcome to Traderfy!\nPlease select the trader you would like to level up from the list below:");
var count = 0;
foreach (var traderName in Enum.GetNames(typeof(TraderName)))
{
    var nameOfTrader = string.Concat(traderName[0].ToString().ToUpper(), traderName[1..]);
    Console.WriteLine($"[{count}] {nameOfTrader}");
    count++;
}
var input = Console.ReadLine();
int selection;
while (int.TryParse(input, out selection) == false || selection < 0 || selection >= Enum.GetNames(typeof(TraderName)).Length)
{
    Console.WriteLine("Invalid selection. Please try again.");
    input = Console.ReadLine();
}
var trader = (TraderName)selection;
Console.Clear();
Console.WriteLine("Now how many roubles must you increase your spending by?");
input = Console.ReadLine();
long lSelection;
while(long.TryParse(input, out lSelection) == false || lSelection < 1)
{
    Console.WriteLine("Invalid selection. Please try again.");
    input = Console.ReadLine();
}


var spending = lSelection;
Console.Clear();
Console.WriteLine("Finally, specify the max number of items you are willing to purchase off the flea market.\n(If you are not sure, just leave this blank and press enter.)");
input = Console.ReadLine();
if (string.IsNullOrEmpty(input))
{
    input = "-1";
}

while(int.TryParse(input, out selection) == false || selection < -1)
{
    Console.WriteLine("Invalid selection. Please try again.");
    input = Console.ReadLine();
}

var maxPurchase = selection;
Console.Clear();









//var orderedItems = items.OrderByDescending(x => x.GetIncomeFrom(trader));
var filteredItems = from item in items
    let net = item.GetIncomeFrom(trader)
    where net != 0
    let price = item.GetPriceFor(trader)
    where price != 0 && price < spending
    let purchaseAmount = (spending / price)
    where purchaseAmount < maxPurchase || maxPurchase == -1
    orderby purchaseAmount * net descending 
    select item;
var bestItem = filteredItems.First();
Console.WriteLine("The best item to buy is {0} at {1} {2} times for {3} and selling it for {4}, you will in total loose {5} roubles.",
    bestItem.Name, bestItem.FleaMarketPrice, spending/bestItem.GetPriceFor(trader), bestItem.FleaMarketPrice * spending/bestItem.GetPriceFor(trader), bestItem.GetPriceFor(trader), (spending/bestItem.GetPriceFor(trader)) * bestItem.GetIncomeFrom(trader) * -1);
Console.WriteLine("\nDone! Press any key to exit...");
Console.ReadKey(true);
/*foreach(var item in orderedItems)
{
    
    var net = item.GetIncomeFrom(trader);
    if (net == 0) continue;
    if (item.FleaMarketPrice < minPrice) continue;
    Console.WriteLine("Reselling {0} to {1} will net {2}", item.Name, trader, net);
    Thread.Sleep(1000);
}*/


/*var cashOffers = await client.GetResponse();

foreach (var item in cashOffers)
{
    Console.WriteLine(item);
}*/
