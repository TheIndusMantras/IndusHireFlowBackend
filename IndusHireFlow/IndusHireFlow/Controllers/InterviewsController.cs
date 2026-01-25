using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/interviews")]
    public class InterviewsController : BaseController
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Success("Auth service is running 🚀");
        }
    }
}
