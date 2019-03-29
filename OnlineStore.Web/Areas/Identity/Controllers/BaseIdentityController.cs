using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    [Area(WebConstants.IdentityAreaName)]
    [Authorize]
    public abstract class BaseIdentityController : BaseController
    {
        
    }
}