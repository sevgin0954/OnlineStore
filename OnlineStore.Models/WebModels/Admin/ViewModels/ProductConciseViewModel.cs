namespace OnlineStore.Models.WebModels.Admin.ViewModels
{
    public class ProductConciseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? PromoPrice { get; set; }

        public int CountsLeft { get; set; }

        public string SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
