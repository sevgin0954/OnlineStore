using OnlineStore.Data;

namespace OnlineStore.Services
{
    public abstract class BaseService
    {
        protected BaseService(OnlineStoreDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected OnlineStoreDbContext DbContext { get; private set; }
    }
}
