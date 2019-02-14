using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Services
{
    public abstract class BaseService
    {
        protected BaseService(OnlineStoreDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected BaseService(OnlineStoreDbContext dbContext, IMapper mapper)
            : this(dbContext)
        {
            this.Mapper = mapper;
        }

        protected BaseService(OnlineStoreDbContext dbContext, UserManager<User> userManager)
            : this(dbContext)
        {
            this.UserManager = userManager;
        }

        protected BaseService(OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : this(dbContext, mapper)
        {
            this.UserManager = userManager;
        }

        protected OnlineStoreDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }

        protected UserManager<User> UserManager { get; private set; }
    }
}
