using System.Text.RegularExpressions;

namespace DBMonitor.BLL.Extensions
{
    public static class StringExtensions
    {

        private const bool USE_WORK_DAYS = Constants.USE_WORK_DAYS;
        /// <summary>
        /// Преобразует строку в формате JIRA/GitLab log work в число секунд
        /// <see href="https://support.atlassian.com/jira-software-cloud/docs/log-time-on-an-issue/"/>
        /// </summary>
        /// <returns>Число секунд, задаваемых строкой</returns>
        public static int FromJiraString(this string s, bool useWorkDays = USE_WORK_DAYS)
        {

            var StringRegex = new Regex(@"^\s*((?<Months>\d+)\s*mo){0,1}\s*((?<Weeks>\d+)\s*w){0,1}\s*((?<Days>\d+)\s*d){0,1}\s*((?<Hours>\d+)\s*h){0,1}\s*((?<Minutes>\d+)\s*m){0,1}$");
            var match = StringRegex.Match(s);
            return !match.Success ? 0 : useWorkDays ? GetTotalSecondsUsingWorkDays(match) : GetTotalSecondsUsingDays(match);
        }

        private static int GetTotalSecondsUsingWorkDays(Match match)
        {
            var origin = DateTime.Now;
            var newDate = origin.AddWorkDays(int.Parse(match.GetGroupValue("Days")));
            newDate = newDate.AddWorkWeeks(int.Parse(match.GetGroupValue("Weeks")));
            newDate = newDate.AddHours(int.Parse(match.GetGroupValue("Hours")));
            newDate = newDate.AddMinutes(int.Parse(match.GetGroupValue("Minutes")));
            newDate = newDate.AddWorkMonths(int.Parse(match.GetGroupValue("Mounths")));
            return (int)(newDate - origin).TotalSeconds;
        }
        private static int GetTotalSecondsUsingDays(Match match)
        {
            var origin = DateTime.Now;
            var newDate = origin.AddDays(int.Parse(match.GetGroupValue("Days")));
            newDate = newDate.AddWeeks(int.Parse(match.GetGroupValue("Weeks")));
            newDate = newDate.AddHours(int.Parse(match.GetGroupValue("Hours")));
            newDate = newDate.AddMinutes(int.Parse(match.GetGroupValue("Minutes")));
            newDate = newDate.AddMonths(int.Parse(match.GetGroupValue("Mounths")));

            var totalSeconds = (int)(newDate - origin).TotalSeconds;
            return totalSeconds;
        }



    }
}
