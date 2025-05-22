namespace Products.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static bool HasMaximumTwoDecimalPlaces(this decimal value)
        {
            return decimal.Round(value, 2) == value;
        }
    }
}
