namespace MiniOrderSystem.Shared.Extensions
{
    public static class OrderExtensions
    {
        public static string CreateOrderNumber(string prefix = "ORD")
        {
            ArgumentNullException.ThrowIfNull(prefix);
            if (prefix.Length != 3) throw new ArgumentException($"{nameof(prefix)} length is 3!");

            int length = 6;
            string text = string.Empty;

            while (text.Length < length)
            {
                text += Guid.NewGuid().ToString().GetHashCode()
                    .ToString("x");
            }

            return $"{prefix.ToLower()}-{text[..length]}";
        }
    }
}
