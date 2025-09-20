using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers
{
    internal record CustomerMessages
    {
        public static readonly Error NameMustBeSent = Error.Validation("InValidFormat", "Name must be sent!");
        public static readonly Error PhoneNumberMustBeSent = Error.Validation("InValidFormat", "PhoneNumber must be sent!");
        public static readonly Error CountryMustBeSent = Error.Validation("InValidFormat", "Country must be sent!");
        public static readonly Error CityMustBeSent = Error.Validation("InValidFormat", "City must be sent!");
        public static readonly Error StreetMustBeSent = Error.Validation("InValidFormat", "Street must be sent!");
        public static readonly Error PostalCodeMustBeSent = Error.Validation("InValidFormat", "PostalCode must be sent!");
        public static readonly Error InvalidPhoneNumberFormat = Error.Validation("InValidFormat", "Invalid PhoneNumber Format!");
        public static readonly Error PhoneNumberIsExist = Error.Failure("DuplicatedData", "This PhoneNumber is used before.");
        public static readonly Error InvalidId = Error.Validation("IdValidation", "Invalid Customer Id.");
        public static readonly Error InvalidName = Error.Validation("NameValidation", "maximum length for Name is 150.");
        public static readonly Error InvalidCountry = Error.Validation("CountryValidation", "maximum length for Country is 100.");
        public static readonly Error InvalidCity = Error.Validation("CityValidation", "maximum length for City is 100.");
        public static readonly Error InvalidStreet = Error.Validation("StreetValidation", "maximum length for Street is 250.");
        public static readonly Error InvalidPostalCodeFormat = Error.Validation("InValidFormat", "Invalid PostalCode Format!");
        public static readonly Error NotFound = Error.NotFound("NotFound", "Customer can not found with requested Id.");
    }
}
