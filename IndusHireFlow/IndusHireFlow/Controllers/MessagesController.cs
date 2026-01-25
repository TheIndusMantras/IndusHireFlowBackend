using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/messages")]
    public class MessagesController : BaseController
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Success("Auth service is running 🚀");
        }
    }
}
