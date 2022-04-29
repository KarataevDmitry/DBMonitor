using System.Text.RegularExpressions;

namespace DBMonitor.BLL.Extensions
{
    public static class MatchExtensions
    {
        public static string GetGroupValue(this Match match, string groupName) =>
                    match.Groups[groupName].Value == "" ? "0" : match.Groups[groupName].Value;
    }
}
