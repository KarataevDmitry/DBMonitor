namespace DBMonitor.BLL.Extensions
{
    public static class DateTimeExtensions
    {
        private const int HOURS_IN_WORK_DAY = Constants.HOURS_IN_WORK_DAY;
        private const int DAYS_IN_WORK_WEEK = Constants.DAYS_IN_WORK_WEEK;
        private const int WEEKS_IN_WORK_MONTH = Constants.WEEKS_IN_WORK_MONTH;
        private const int DAYS_IN_WEEK = Constants.DAYS_IN_WEEK;
        /// <summary>
        /// Добавляет рабочие дни к дате
        /// </summary>
        /// <param name="daysCount">Количество рабочих дней, которые нужно добавить</param>
        /// <returns><c>DateTime</c> к которому добавлено <c>daysCount</c> рабочих дней</returns>
        public static DateTime AddWorkDays(this DateTime d, int daysCount) => d.AddHours(HOURS_IN_WORK_DAY * daysCount);
        /// <summary>
        /// Добавляет рабочие недели к дате
        /// </summary>
        /// <param name="weeksCount">Количество рабочих недель (40ч), которые нужно добавить</param>
        /// <returns><c>DateTime</c> к которому добавлено <c>weeksCount</c> рабочих недель</returns>
        public static DateTime AddWorkWeeks(this DateTime d, int weeksCount) => d.AddWorkDays(DAYS_IN_WORK_WEEK * weeksCount);
        public static DateTime AddWeeks(this DateTime d, int weeksCount) => d.AddWorkDays(DAYS_IN_WEEK * weeksCount);
        /// <summary>
        /// Добавляет рабочие недели к дате
        /// </summary>
        /// <param name="monthsCount">Количество рабочих месяцев, которые нужно добавить</param>
        /// <returns><c>DateTime</c> к которому добавлено <c>monthsCount</c> рабочих месяцев</returns>
        public static DateTime AddWorkMonths(this DateTime d, int monthsCount) => d.AddWorkWeeks(WEEKS_IN_WORK_MONTH * monthsCount);
    }
}
