using System;
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

        /// <summary>
        /// Parses the given string into a pair of first name and last name,
        /// or uses the given first name and last name.
        ///
        /// Usage:
        /// ParseFullName("tan,rong", null, null, true, ',');
        /// ParseFullName("Rong Tan", null, null, false, ' ');
        /// </summary>
        /// <param name="fullName">Full name</param>
        /// <param name="firstName">Default first name</param>
        /// <param name="lastName">Default last name</param>
        /// <param name="firstNameLeading">Last name leads in the given fullName or not</param>
        /// <param name="separator">The separator default to be ,</param>
        /// <returns>A pair of names</returns>
        public static Tuple<string, string> ParseFullName(this string fullName,
            string firstName = null, string lastName = null,
            bool firstNameLeading = false, char separator=',')
        {
            // If first name or last name is provider, use them directly.
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            {
                firstName = firstName ?? "Name";
                lastName = lastName ?? "Surname";
                return new Tuple<string, string>(firstName, lastName);
            }

            // fullName has value.
            if (!string.IsNullOrEmpty(fullName))
            {
                fullName = fullName.Trim();

                var idx = fullName.IndexOf(separator);
                if (idx != -1)
                {
                    if (firstNameLeading)
                    {
                        lastName = fullName.Substring(0, idx)?.Trim();
                        firstName = fullName.Substring(idx + 1)?.Trim();
                    }
                    else
                    {
                        firstName = fullName.Substring(0, idx)?.Trim();
                        lastName = fullName.Substring(idx + 1)?.Trim();
                    }
                }
            }

            firstName = firstName ?? "Name";
            lastName = lastName ?? "Surname";

            return new Tuple<string, string>(firstName, lastName);
        }
    }
}

