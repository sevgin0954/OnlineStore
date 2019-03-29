namespace OnlineStore.Common.Constants
{
    public class WebConstants
    {
        public const string IdentityAreaName = "Identity";

        public const string AdminAreaName = "Admin";

        public const string AdminRoleName = "Administrator";

        public const string QuestAreaName = "Quest";

        public const string LecturerAreaRolesNames = "Administrator, Lecturer";

        public const string ConnectionString = "OnlineStoreDbContextConnection";

        public const string DefaultAdminUsername = "admin";

        public const string DefaultAdminEmail = "admin@admin.com";

        public const string DefaultAdminPassword = "123456789";

        public const string SessionProductsKey = "ProductsSessionKey";

        public const string OrderStatusOnTheWay = "On the way";

        public const string OrderStatusCanceled = "Canceled";

        public const string OrderStatusDelivered = "Delivered";

        public const int BanUserDays = 365;

        public const int SessionIdleTimeoutDays = 10;

        public const int ExpiredItemsDeletionIntervalHours = 1;

        public const int DeliveryRequiredDays = 2;

        public const decimal DeliveryPrice = 5;

        public const int DaysPastToCountAsNewUser = 1;

        public const int DaysPastToCountAsNewOrder = 1;
    }
}
