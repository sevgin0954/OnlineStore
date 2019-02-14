namespace OnlineStore.Models.WebModels.Admin.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int OrdersCount { get; set; }

        public bool IsBanned { get; set; }
    }
}
