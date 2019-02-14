namespace OnlineStore.Models.WebModels.Admin.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? PromoPrice { get; set; }

        public int CountsLeft { get; set; }

        public long OrdersCount { get; set; }

        public string SubCategoryName { get; set; }
    }
}
