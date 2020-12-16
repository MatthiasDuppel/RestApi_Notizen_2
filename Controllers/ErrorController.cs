using Microsoft.AspNetCore.Mvc;

namespace NotizenApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        protected IActionResult Error() => Problem();
    }
}
