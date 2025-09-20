namespace MiniOrderSystem.Presentation.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/api/ping/pong")]
        public IActionResult Pong() => Ok(new { pong_at = DateTime.Now });
    }
}
