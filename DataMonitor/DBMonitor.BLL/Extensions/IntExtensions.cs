using System.Text;

namespace DBMonitor.BLL.Extensions
{
    public static class IntExtensions
    {
        private const int HOURS_IN_WORK_MONTH = Constants.HOURS_IN_WORK_MONTH;
        private const int HOURS_IN_WORK_DAY = Constants.HOURS_IN_WORK_DAY;
        private const int HOURS_IN_WORK_WEEK = Constants.HOURS_IN_WORK_WEEK;
        /// <summary>
        /// Показывает поведение по умолчанию. Используются ли рабочие дни для расчетов 
        /// (<c>True</c>) или обычные (<c>False</c>) 
        /// </summary>
        private const bool USE_WORK_DAYS = Constants.USE_WORK_DAYS;
        /// <summary>
        /// Конвертирует число (воспринимая его как количество секунд затраченных на работу) в формат JIRA/GitLab
        /// https://support.atlassian.com/jira-software-cloud/docs/log-time-on-an-issue/
        /// </summary>
        /// <returns>Строка</returns>
        public static string AsJiraString(this int i, bool useWorkDays = USE_WORK_DAYS)
        {
            GetLogTimeEntries(i, out var monthsCount, out var weeksCount, out var daysCount, out var hoursCount, out var minutesCount, useWorkDays);
            return FormatJiraString(monthsCount, weeksCount, daysCount, hoursCount, minutesCount);

        }

        private static void GetLogTimeEntries(int i, out int monthsCount, out int weeksCount, out int daysCount, out int hoursCount, out int minutesCount, bool useWorkDays)
        {

            CalculateTimeSpans(i, out var ts, out var monthsTimeSpan, out var daysTimeSpan, out var weeksTimeSpan, out var hoursTimeSpan, useWorkDays);
            CalculateLogTimeEntries(out monthsCount, out weeksCount, out daysCount, out hoursCount, out minutesCount, ts, monthsTimeSpan, daysTimeSpan, weeksTimeSpan, hoursTimeSpan);
        }

        private static void CalculateLogTimeEntries(out int monthsCount, out int weeksCount, out int daysCount, out int hoursCount, out int minutesCount, TimeSpan ts, TimeSpan monthsTimeSpan, TimeSpan daysTimeSpan, TimeSpan weeksTimeSpan, TimeSpan hoursTimeSpan)
        {
            monthsCount = (int)ts.Divide(monthsTimeSpan);
            var rest = ts - (monthsCount * monthsTimeSpan);
            weeksCount = (int)rest.Divide(weeksTimeSpan);
            rest -= (weeksCount * weeksTimeSpan);
            daysCount = (int)rest.Divide(daysTimeSpan);
            rest -= daysCount * daysTimeSpan;
            hoursCount = (int)rest.Divide(hoursTimeSpan);
            rest -= hoursCount * hoursTimeSpan;
            minutesCount = (int)rest.TotalMinutes;
        }

        private static void CalculateTimeSpans(int i, out TimeSpan ts, out TimeSpan monthsTimeSpan, out TimeSpan daysTimeSpan, out TimeSpan weeksTimeSpan, out TimeSpan hoursTimeSpan, bool useWorkDays = USE_WORK_DAYS)
        {

            ts = TimeSpan.FromSeconds(i);
            GetDaysTimeSpans(out monthsTimeSpan, out daysTimeSpan, out weeksTimeSpan, useWorkDays);
            hoursTimeSpan = TimeSpan.FromHours(1);
        }

        private static void GetDaysTimeSpans(out TimeSpan monthsTimeSpan, out TimeSpan daysTimeSpan, out TimeSpan weeksTimeSpan, bool useWorkDays = USE_WORK_DAYS)
        {
            if (useWorkDays)
            {
                GetTimeSpansUsingWorkDays(out monthsTimeSpan, out daysTimeSpan, out weeksTimeSpan);
            }
            else
            {
                GetTimeSpansUsingDays(out monthsTimeSpan, out daysTimeSpan, out weeksTimeSpan);
            }
        }

        private static void GetTimeSpansUsingDays(out TimeSpan monthsTimeSpan, out TimeSpan daysTimeSpan, out TimeSpan weeksTimeSpan)
        {
            monthsTimeSpan = TimeSpan.FromDays(27.3);
            daysTimeSpan = TimeSpan.FromDays(1);
            weeksTimeSpan = TimeSpan.FromDays(7);
        }

        private static void GetTimeSpansUsingWorkDays(out TimeSpan monthsTimeSpan, out TimeSpan daysTimeSpan, out TimeSpan weeksTimeSpan)
        {
            monthsTimeSpan = TimeSpan.FromHours(HOURS_IN_WORK_MONTH);
            daysTimeSpan = TimeSpan.FromHours(HOURS_IN_WORK_DAY);
            weeksTimeSpan = TimeSpan.FromHours(HOURS_IN_WORK_WEEK);
        }

        private static string FormatJiraString(int monthsCount, int weeksCount, int daysCount, int hoursCount, int minutesCount)
        {
            var sb = new StringBuilder();
            if (monthsCount > 0)
            {
                sb.Append($"{monthsCount}mo");
            }
            if (weeksCount > 0)
            {
                sb.Append($"{weeksCount}w");
            }
            if (daysCount > 0)
            {
                sb.Append($"{daysCount}d");
            }
            if (hoursCount > 0)
            {
                sb.Append($"{hoursCount}h");
            }
            if (minutesCount > 0)
            {
                sb.Append($"{minutesCount}m");
            }
            var v = sb.ToString();
            return v;
        }
    }
}
