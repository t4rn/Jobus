using Jobus.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Jobus.Common.Extensions
{
    public static class GenericExtensions
    {
        public static string Serialize<T>(this T instance, SerializeFormat format = SerializeFormat.Json)
        {
            string result = string.Empty;

            if (instance != null)
            {
                switch (format)
                {
                    case SerializeFormat.Json:
                        result = JsonConvert.SerializeObject(instance, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        break;

                    case SerializeFormat.Xml:
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        using (StringWriter textWriter = new StringWriter())
                        {
                            xmlSerializer.Serialize(textWriter, instance);
                            result = textWriter.ToString();
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }

        public static bool IsOneOf<T>(this T element, params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(element))
                    return true;
            }

            return false;
        }

        public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static bool AnyExists<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }
    }
}
