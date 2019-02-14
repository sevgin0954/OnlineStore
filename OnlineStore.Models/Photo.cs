namespace OnlineStore.Models
{
    public class Photo
    {
        public string Id { get; set; }

        public byte[] Data { get; set; }

        public User User { get; set; }

        public string ReviewId { get; set; }
        public Review Review { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}