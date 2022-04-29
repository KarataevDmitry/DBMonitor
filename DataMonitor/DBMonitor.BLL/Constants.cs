namespace DBMonitor.BLL
{
    public static class Constants
    {
        /// <summary>
        /// <c>True</c> если для расчетов нужно использовать рабочие дни
        /// <c>False</c> - обычные (7 дней в неделе)
        /// </summary>
        public const bool USE_WORK_DAYS = true;
        public const int DAYS_IN_WORK_WEEK = 5;
        public const int HOURS_IN_WORK_MONTH = 4 * HOURS_IN_WORK_WEEK;
        public const int HOURS_IN_WORK_DAY = 8;
        public const int HOURS_IN_WORK_WEEK = 40;
        public const int WEEKS_IN_WORK_MONTH = 4;
        public const int DAYS_IN_WEEK = 7;
    }
}
