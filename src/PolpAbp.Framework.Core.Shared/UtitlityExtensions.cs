using System.Text.RegularExpressions;

namespace PolpAbp.Framework
{
    public static class UtitlityExtensions
    {
        public static string MaskEmailAddress(this string input)
        {
            string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(input, pattern, m => new string('*', m.Length));
            return result;
        }

        public static string MaskPhoneNumber(this string str)
        {
            var temp = Regex.Replace(str, "[0-9]", "*");
            str = temp + str.Substring(str.Length - 4, str.Length - (str.Length - 4));
            return str;
        }

        public static string ComposeFullName(string firstName, string lastName, string deaultValue = "User")
        {
            return string.IsNullOrEmpty(firstName) ? $"{lastName ?? deaultValue}"
                : $"{firstName} {lastName ?? ""}";
        }
    }
}

