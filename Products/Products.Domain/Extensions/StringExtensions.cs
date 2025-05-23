namespace Products.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEqual(this string value, string str2)
        {
            return value.ToLower().Trim().Equals(str2.ToLower().Trim());
        }
    }
}
