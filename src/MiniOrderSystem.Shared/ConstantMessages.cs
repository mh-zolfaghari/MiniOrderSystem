using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Shared
{
    public static class ConstantMessages
    {
        public static readonly Error SaveChangesFailed = Error.Exception("SaveChangesFailed", "Database operation failed :(");
        public static readonly Error DeleteRecordFailed = Error.NotFound("DeleteRecordFailed", "It is not possible to delete the desired record :(");
        public static readonly Error InsertRecordFailed = Error.NotFound("InsertRecordFailed", "Unable to add the desired record :(");
        public static readonly Error UpdateRecordFailed = Error.NotFound("UpdateRecordFailed", "Unable to update the desired record :(");
    }
}
