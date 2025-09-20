namespace MiniOrderSystem.Shared.Result
{
    public enum ErrorType
    {
        [Description("UnAuthorized")]
        UnAuthorized = 401,

        [Description("Forbidden")]
        Forbidden = 403,

        [Description("Bad Request")]
        Validation = 400,

        [Description("Internal Server Error")]
        Failure = 500,

        [Description("Not Found")]
        NotFound = 404,

        [Description("No Warning and Error")]
        None = 100
    }
}
