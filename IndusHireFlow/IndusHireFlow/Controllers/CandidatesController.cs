using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/candidates")]
    public class CandidatesController : BaseController
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Success("Auth service is running 🚀");
        }
    }
}
