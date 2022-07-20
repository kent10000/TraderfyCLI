using Newtonsoft.Json;

namespace wrappertest.TarkovSharp
{
    public partial class TraderPrice
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avg24hPrice")]
        public long Avg24HPrice { get; set; }

        [JsonProperty("sellFor")]
        public SellFor[] SellFor { get; set; }
    }

    public partial class SellFor
    {
        [JsonProperty("vendor")]
        public Vendor Vendor { get; set; }

        [JsonProperty("priceRUB")]
        public long PriceRub { get; set; }
    }

    public partial class Vendor
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}