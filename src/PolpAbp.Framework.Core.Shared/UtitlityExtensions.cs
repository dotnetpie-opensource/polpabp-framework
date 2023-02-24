using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
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
            bool firstNameLeading = false, char separator = ',')
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

        public static string RemoveDiacritics(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            string formD = s.Normalize(NormalizationForm.FormD);
            char[] chars = new char[formD.Length];
            int count = 0;
            foreach (char c in formD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    chars[count++] = c;
                }
            }
            string noDiacriticsFormD = new string(chars, 0, count);
            return noDiacriticsFormD.Normalize(NormalizationForm.FormC);
        }

        public static string MakeSubdomain(this string rawSubdomain, string baseDomain)
        {
            if (baseDomain.Length + 2 > 253)
            {
                throw new ArgumentException("Base domain is already too long for a subdomain");
            }
            if (baseDomain.Length == 0)
            {
                throw new ArgumentException("Invalid base domain");
            }

            var sub = rawSubdomain.RemoveDiacritics();
            sub = Regex.Replace(sub, @"[^a-zA-Z0-9-]+", "");
            sub = Regex.Replace(sub, @"(^-+)|(-+$)", "");
            sub = sub.ToLowerInvariant();

            if (sub.Length > 63)
            {
                sub = sub.Substring(0, 63);
            }
            if (sub.Length + baseDomain.Length + 1 > 253)
            {
                sub = sub.Substring(0, 252 - baseDomain.Length);
            }
            return sub + "." + baseDomain;
        }

        public static bool IsValidIdentifier(this string text)
        {
            var regex = new Regex(@"^[a-zA-Z](([a-zA-Z0-9]*)|(_[a-zA-Z0-9]_*)|(_[a-zA-Z0-9]*)|([a-zA-Z0-9]_*))$");
            return regex.IsMatch(text);
        }

        public static string AttemptParseOutUserName(this string text)
        {
            var username = "";
            if (text.Contains("@"))
            {
                var addr = new MailAddress(text);
                username = addr.User;
            }

            if (string.IsNullOrEmpty(username))
            {
                var sub = text.RemoveDiacritics();
                sub = Regex.Replace(sub, @"[^a-zA-Z0-9-]+", "");
                sub = Regex.Replace(sub, @"(^-+)|(-+$)", "");
                username = sub.ToLowerInvariant();

            }
            if (username.Length > 63)
            {
                username = username.Substring(0, 63);
            }

            return username;
        }

        public static string ReplaceSorting(this string sorting, Func<string, string> replaceFunc)
        {
            var sortFields = sorting.Split(',');
            for (var i = 0; i < sortFields.Length; i++)
            {
                sortFields[i] = replaceFunc(sortFields[i].Trim());
            }

            return string.Join(",", sortFields);
        }
    }
}

