using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Extensions
{
    public static class Monetary
    {

        public static string ToRand(this double value)
        {
            return value.ToString("C", CultureInfo.CreateSpecificCulture("en-ZA"));
        }

        public static string ToRand(this decimal value)
        {
            return value.ToString("C", CultureInfo.CreateSpecificCulture("en-ZA"));
        }

    }
}
