using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobus.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidUri(this string str)
        {
            return Uri.TryCreate(str, UriKind.Absolute, out Uri validatedUri);
        }

        public static string Shorten(this string str, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.Length > maxLength)
                return str.Substring(0, maxLength);

            return str;
        }
    }
}
