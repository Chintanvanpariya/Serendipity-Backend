using Serendipity.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Serendipity.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}