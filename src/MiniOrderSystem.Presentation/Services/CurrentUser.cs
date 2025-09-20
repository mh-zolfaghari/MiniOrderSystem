using MiniOrderSystem.Domain.Common;

namespace MiniOrderSystem.Presentation.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid? Token
        {
            get
            {
                var token = _httpContextAccessor.HttpContext?.Request?.Headers["Token"];
                return token is not null && Guid.TryParse(token, out var _token) ? _token : null;
            }
        }
    }
}
