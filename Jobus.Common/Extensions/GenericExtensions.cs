using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobus.Common.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T element, params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(element))
                    return true;
            }

            return false;
        }

    }
}
