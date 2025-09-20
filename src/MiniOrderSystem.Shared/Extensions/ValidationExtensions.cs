namespace MiniOrderSystem.Shared.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsMobileNo(string? mobileNo)
        {
            if (string.IsNullOrWhiteSpace(mobileNo))
                return false;

            try { return new Regex("^(98|\\+98|0098|0)?9\\d{9}$").IsMatch(mobileNo.ToEnglishDigit()!); }
            catch { return false; }
        }

        public static bool IsPostalCode(string? postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return false;

            try { return new Regex(@"^\d{10}$").IsMatch(postalCode.ToEnglishDigit()!); }
            catch { return false; }
        }
    }
}
