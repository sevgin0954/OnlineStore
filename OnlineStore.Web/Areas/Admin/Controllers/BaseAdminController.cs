using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminAreaName)]
    [Authorize(Roles = WebConstants.AdminRoleName)]
    public abstract class BaseAdminController : BaseController
    {
        
    }
}