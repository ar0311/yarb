using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System;

namespace Yarb.Web.Blog.Utilities
{
    public static class SlugConverter
    {
        /// <summary>
        /// Slugifies a string
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <returns>A URL safe slug representation of the input <paramref name="value"/>.</returns>
        public static string ToSlug(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (SlugRegex.IsMatch(value))
            {
                return value;
            }

            return GenerateSlug(value);
        }

        /// <summary>
        /// A regular expression for validating slugs.
        /// Does not allow leading or trailing hypens or whitespace.
        /// </summary>
        private static readonly Regex SlugRegex = new Regex(@"(^[a-z0-9])([a-z0-9_-]+)*([a-z0-9])$", RegexOptions.Compiled);

        // Implementation credits go to Ayende Rahien's "RacoonBlog"
        // https://github.com/ayende/RaccoonBlog
        public static string GenerateSlug(string title)
        {
            title = HttpUtility.HtmlDecode(title);

            // 2 - Strip diacritical marks using Michael Kaplan's function or equivalent
            title = RemoveDiacritics(title);

            // 3 - Lowercase the string for canonicalization
            title = title.ToLowerInvariant();

            // 4 - Replace all the non-word characters with dashes
            title = ReplaceNonWordWithDashes(title);

            // 1 - Trim the string of leading/trailing whitespace
            title = title.Trim(' ', '-');

            return title;
        }

        // http://blogs.msdn.com/michkap/archive/2007/05/14/2629747.aspx
        /// <summary>
        /// Strips the value from any non English character by replacing those with their English equivalent.
        /// </summary>
        /// <param name="value">The string to normalize.</param>
        /// <returns>A string where all characters are part of the basic English ANSI encoding.</returns>
        /// <seealso cref="http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net"/>
        private static string RemoveDiacritics(string value)
        {
            string stFormD = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        private static string ReplaceNonWordWithDashes(string title)
        {
            // Remove Apostrophe Tags
            title = Regex.Replace(title, "[’'“”\"&]{1,}", "", RegexOptions.None);

            // Replaces all non-alphanumeric character by a space
            var builder = new StringBuilder();
            for (int i = 0; i < title.Length; i++)
            {
                builder.Append(char.IsLetterOrDigit(title[i]) ? title[i] : ' ');
            }

            title = builder.ToString();

            // Replace multiple spaces into a single dash
            title = Regex.Replace(title, "[ ]{1,}", "-", RegexOptions.None);

            return title;
        }
    }
}