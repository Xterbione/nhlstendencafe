namespace nhl_stenden_cafe.Models
{
    public class ProductCountsOrder
    {
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public int ProductCountPaid { get; set; }
        public int ProductCountToPay { get; set; }
        public int ProductCountNotPaid { get; set; }

    }
}


