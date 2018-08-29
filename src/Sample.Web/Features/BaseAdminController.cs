using Microsoft.AspNetCore.Mvc;

namespace Sample.Web.Features
{
    [Route("api/admin/[controller]")]
    public class BaseAdminController : BaseAuthorizeController
    {
    }
}
