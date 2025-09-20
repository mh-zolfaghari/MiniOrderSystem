using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products
{
    internal record ProductMessages
    {
        public static readonly Error NameMustBeSent = Error.Validation("InValidFormat", "Name must be sent!");
        public static readonly Error InvalidName = Error.Validation("NameValidation", "maximum length for Name is 300.");
        public static readonly Error InvalidDescription = Error.Validation("DescriptionValidation", "maximum length for Description is 3000.");
        public static readonly Error InvalidPrice = Error.Validation("PriceValidation", "The price of the item must be greater than 0.");
        public static readonly Error NotFound = Error.NotFound("NotFound", "Product can not found with requested Id.");
        public static readonly Error InvalidId = Error.Validation("IdValidation", "Invalid Product Id.");
    }
}
