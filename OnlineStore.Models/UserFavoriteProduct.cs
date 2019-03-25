namespace OnlineStore.Models
{
    public class UserFavoriteProduct
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
