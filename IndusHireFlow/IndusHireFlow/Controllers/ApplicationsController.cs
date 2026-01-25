using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/applcations")]
    public class ApplicationsController : BaseController
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Success("Auth service is running 🚀");
        }
    }
}
