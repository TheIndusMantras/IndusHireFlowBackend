using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/jobs")]
    public class JobsController : BaseController
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Success("Auth service is running 🚀");
        }
    }
}
