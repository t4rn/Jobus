using Jobus.Common.Enums;
using Jobus.Common.Results;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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

        public static bool HasCertainLength(this string str, int from, int to)
        {
            return str.Length >= from && str.Length <= to;
        }

        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str)) { return str; }

            var startUnderscores = Regex.Match(str, @"^_+");
            return startUnderscores + Regex.Replace(str, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static string ToCamelCase(this string str)
        {
            if (str == null || str.Length < 2)
                return str;

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string CleanWhiteSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static T Deserialize<T>(this string instance, SerializeFormat format = SerializeFormat.Json)
        {
            switch (format)
            {
                case SerializeFormat.Json:
                    return JsonConvert.DeserializeObject<T>(instance);

                case SerializeFormat.Xml:
                    using (var stringReader = new StringReader(instance))
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(stringReader);
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        #region NIP

        public static Result IsNip(this string instance)
        {
            Result result = new Result();

            long tempOutVar;
            if (instance == null || instance.Length != 10)
            {
                result.Message = "Nip has incorrect length";
            }
            else if (instance.Contains(" "))
            {
                result.Message = "Nip has spaces";
            }
            else if (!long.TryParse(instance, out tempOutVar))
            {
                result.Message = "Nip should contain digits only.";
            }
            else if (!IsCorrectNip(instance))
            {
                result.Message = "Nip has incorrect checksum.";
            }
            else
            {
                result.IsOk = true;
            }

            return result;
        }

        private static bool IsCorrectNip(string str)
        {
            int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            bool result = false;
            if (str.Length == 10)
            {
                int controlSum = CalculateControlSum(str, weights);
                int controlNum = controlSum % 11;
                int lastDigit = int.Parse(str[str.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }

        #endregion

        #region PESEL

        public static Result IsPesel(this string instance)
        {
            Result result = new Result();

            long tempOutVar;
            if (instance == null || instance.Length != 11)
            {
                result.Message = "Pesel has incorrect length";
            }
            else if (instance.Contains(" "))
            {
                result.Message = "Pesel has spaces";
            }
            else if (!long.TryParse(instance, out tempOutVar))
            {
                result.Message = "Pesel should contain digits only.";
            }
            else if (!IsCorrectPesel(instance))
            {
                result.Message = "Pesel has incorrect checksum.";
            }
            else
            {
                result.IsOk = true;
            }

            return result;
        }

        private static bool IsCorrectPesel(string str)
        {
            int sum = 0;
            int[] peselFactors = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };

            for (int i = 0; i < str.Length; i++)
            {
                sum += int.Parse(str[i].ToString()) * peselFactors[i];
            }

            return sum % 10 == 0 && sum != 0;
        }

        #endregion

        #region REGON

        public static Result IsRegon(this string instance)
        {
            Result result = new Result();

            long tempOutVar;
            if (instance == null || !instance.Length.IsOneOf(9, 14))
            {
                result.Message = "Regon has incorrect length";
            }
            else if (instance.Contains(" "))
            {
                result.Message = "Regon has spaces";
            }
            else if (!long.TryParse(instance, out tempOutVar))
            {
                result.Message = "Regon should contain digits only.";
            }
            else if (!IsCorrectRegon(instance))
            {
                result.Message = "Pesel has incorrect checksum.";
            }
            else
            {
                result.IsOk = true;
            }

            return result;
        }

        private static bool IsCorrectRegon(string str)
        {
            int[] weights9 = { 8, 9, 2, 3, 4, 5, 6, 7 };
            int[] weights14 = { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
            int controlSum9;
            int controlSum14;
            bool isOk = false;

            string tempRegon9 = str;

            if (str.Length == 14)
            {
                tempRegon9 = str.Substring(0, 9);
            }

            controlSum9 = CalculateControlSum(tempRegon9, weights9);
            int controlNum9 = controlSum9 % 11;
            if (controlNum9 == 10)
            {
                controlNum9 = 0;
            }
            int digit9 = int.Parse(str[tempRegon9.Length - 1].ToString());

            isOk = digit9 == controlNum9;

            if (isOk && str.Length == 14)
            {
                controlSum14 = CalculateControlSum(str, weights14);
                int controlNum14 = controlSum14 % 11;
                if (controlNum14 == 10)
                {
                    controlNum14 = 0;
                }
                int digit14 = int.Parse(str[str.Length - 1].ToString());
                isOk = digit14 == controlNum14;
            }
            return isOk;
        }

        #endregion
    }
}
