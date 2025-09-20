using MiniOrderSystem.Presentation.Extensions;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController(ISender sender) : Controller
    {
        protected readonly ISender MediatR = sender;

        protected IActionResult OK<T>(Result<T> response)
        {
            if (response.Error.Type == ErrorType.None)
                return Ok(response);

            return CreateProblemResult(response.MapToProblemDetails());
        }

        protected IActionResult OK(Result response)
        {
            if (response.Error.Type == ErrorType.None)
                return Ok(response);

            return CreateProblemResult(response.MapToProblemDetails());
        }

        private IActionResult CreateProblemResult(ProblemDetails problemDetails)
        {
            return Problem
                (
                    detail: problemDetails.Detail,
                    instance: problemDetails.Instance,
                    statusCode: problemDetails.Status,
                    title: problemDetails.Title,
                    type: problemDetails.Type
                );
        }
    }
}
