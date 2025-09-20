using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders
{
    internal record OrderMessages
    {
        public static readonly Error NotFound = Error.NotFound("NotFound", "Order can not found with requested ProductId and CustomerId.");
        public static readonly Error AccessDenied = Error.Forbbiden("AccessDenied", "You do not have access to this order.");
        public static readonly Error InvalidQuantity = Error.Validation("QuantityValidation", "The Quantity of the item must be greater than 0.");
        public static readonly Error InvalidId = Error.Validation("IdValidation", "Invalid Order Id.");
        public static readonly Error InvalidOrderNumber = Error.Validation("OrderNumberValidation", "Standard length for OrderNumber is 10.");
        public static readonly Error InvalidStatus = Error.Validation("IdValidation", "Invalid Status value.");
        public static readonly Error OrderNumberMustBeSent = Error.Validation("InValidFormat", "OrderNumber must be sent!");
    }
}
