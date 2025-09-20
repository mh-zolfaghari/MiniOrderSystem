namespace MiniOrderSystem.Application.Common.Exceptions
{
    public class ClientUnAuthorizedException : Exception
    {
        public ClientUnAuthorizedException() : base("Token is not Valid! :(")
        {
        }
    }
}
