namespace BE.Models
{
    public class BestSellingProduct
    {
        public string ProductId { get; set; }
        public int TotalQuantitySold { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }

    }
}
