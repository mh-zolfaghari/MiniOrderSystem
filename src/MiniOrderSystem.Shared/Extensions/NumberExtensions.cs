namespace MiniOrderSystem.Shared.Extensions
{
    public static class NumberExtensions
    {
        public static string? ToEnglishDigit(this string? textNumber)
        {
            if (string.IsNullOrWhiteSpace(textNumber))
                return textNumber;

            return new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9",
                ["٤"] = "4",
                ["٥"] = "5",
                ["٦"] = "6"
            }.Aggregate(textNumber, (string current, KeyValuePair<string, string> item) => current.Replace(item.Key, item.Value));
        }
    }
}
