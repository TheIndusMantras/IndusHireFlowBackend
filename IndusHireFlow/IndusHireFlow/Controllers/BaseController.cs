using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IndusHireFlow.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        // 🔐 Logged-in User Id
        protected Guid UserId =>
            User?.Identity?.IsAuthenticated == true
                ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                : Guid.Empty;

        // 🏢 Company Id (from JWT claim)
        protected Guid CompanyId =>
            User?.Identity?.IsAuthenticated == true
                ? Guid.Parse(User.FindFirst("CompanyId")!.Value)
                : Guid.Empty;

        // ✅ Standard Success Response
        protected IActionResult Success(object data, string message = "Success")
        {
            return Ok(new
            {
                success = true,
                message,
                data
            });
        }

        // ❌ Standard Error Response
        protected IActionResult Failure(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new
            {
                success = false,
                message
            });
        }
    }

// AuthController.cs done
//CandidatesController.cs
//JobsController.cs
//ApplicationsController.cs
//InterviewsController.cs
//OffersController.cs
//MessagesController.cs
//DashboardController.cs
//AIInsightsController.cs
//AnalyticsController.cs
}
