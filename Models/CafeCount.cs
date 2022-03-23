namespace nhl_stenden_cafe.Pages.Repositorys
{
    public class CafeCount
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public int ProductCountPaid { get; set; }
        public int ProductCountNotPaid { get; set; }
        public int ProductCountToPay { get; set; }
        public decimal Price { get; set; }
        public int ProductCountSeparate { get; set; }

        public decimal ProductTotal
        {
            get
            {
                return (Price * ProductCountNotPaid);
            }
        }
    }
}
