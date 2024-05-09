namespace CommonEx.Utilities.DateTimeUtilities.Extensions
{
    public static class TimeSpanExtensions
    {
        private const double DaysOfYear = 365;
        private const double MonthsOfYear = 12;
        private const double DaysOfWeek = 7;
        private const double DaysOfMonth = DaysOfYear / MonthsOfYear;

        /// <summary>
        /// 多少年
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetTotalYears(this TimeSpan time)
        {
            return time.TotalDays / DaysOfYear;
        }

        /// <summary>
        /// 多少個月
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetTotalMonths(this TimeSpan time)
        {
            return time.TotalDays / DaysOfMonth;
        }

        /// <summary>
        /// 多少週
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetTotleWeeks(this TimeSpan time)
        {
            return time.TotalDays / DaysOfWeek;
        }
    }
}
