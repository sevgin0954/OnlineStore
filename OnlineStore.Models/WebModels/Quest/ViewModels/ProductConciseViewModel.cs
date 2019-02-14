namespace OnlineStore.Models.WebModels.Quest.ViewModels
{
    public class ProductConciseViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? PromoPrice { get; set; }

        public int CountsLeft { get; set; }

        public string Description { get; set; }

        public byte[] MainPhoto { get; set; }

        public int ReviewsCount { get; set; }

        public int ReviewsAvgStartRating { get; set; }
    }
}
