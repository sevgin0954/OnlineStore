namespace OnlineStore.Models.WebModels.Quest.ViewModels
{
    public class ProductShoppingCartViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Photo MainPhoto { get; set; }

        public int Count { get; set; }
    }
}
