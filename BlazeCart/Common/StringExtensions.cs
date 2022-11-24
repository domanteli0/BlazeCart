using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtensions
    {


        /// <summary>
        /// Returns first match based on a specified pattern.
        /// </summary>
        /// <returns>First regex match</returns>
        public static string FindFirstRegexMatch(this string str, string pattern)
        {
            return (new Regex(pattern)).Matches(str).First().ToString();
        }

        /// <summary>
        /// Returns all matches based on a specified pattern.
        /// </summary>
        public static MatchCollection FindRegexMatches(this string str, string pattern)
        {
            return (new Regex(pattern)).Matches(str);
        }

        /// <summary>
        /// Returns a string repeated itself `count` ammount of times
        /// </summary>
        public static string Times(this string str, int count)
        {
            var ret = "";
            for (int i = 0; i < count; i++)
                ret += str;

            return ret;
        }
    }
}
