namespace nhl_stenden_cafe.Models
{
    public class Order
    {
        public string orderid { get; set; }
        public int tafelnr { get; set; }
        public bool isplaced { get; set; } = false;
        public Dictionary<int, int> items { get; set; } = new Dictionary<int, int>();

    }
}
