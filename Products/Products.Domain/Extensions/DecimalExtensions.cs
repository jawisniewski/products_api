using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
