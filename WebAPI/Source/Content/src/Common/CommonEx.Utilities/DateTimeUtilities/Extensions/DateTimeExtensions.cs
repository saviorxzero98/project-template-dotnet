using System.Globalization;

namespace CommonEx.Utilities.DateTimeUtilities.Extensions
{
    public static class DateTimeExtensions
    {
        #region Commons

        /// <summary>
        /// 是否為空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this DateTime value)
        {
            return (value == default(DateTime));
        }

        #endregion


        #region Date Range

        /// <summary>
        /// Get First Day Of Year
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Year
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastDayOfYear(this DateTime value)
        {
            return value.FirstDayOfYear().AddYears(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Get First Day Of Month
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Month
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return value.FirstDayOfMonth().AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Day Of Week
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(this DateTime value)
        {
            DayOfWeek dayOfWeek = value.DayOfWeek;
            return value.FirstTimeOfDay().AddDays((int)DayOfWeek.Sunday - (int)dayOfWeek);
        }

        /// <summary>
        /// Last Day Of Week
        /// </summary>
        public static DateTime LastDayOfWeek(this DateTime value)
        {
            DayOfWeek dayOfWeek = value.DayOfWeek;
            return value.LastTimeOfDay().AddDays((int)DayOfWeek.Saturday - (int)dayOfWeek);
        }

        /// <summary>
        /// First Millisecond Of Day
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Day
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfDay(this DateTime value)
        {
            return value.FirstTimeOfDay().AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Hour
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfHour(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Hour
        /// </summary>
        public static DateTime LastTimeOfHour(this DateTime value)
        {
            return value.FirstTimeOfHour().AddHours(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Minute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfMinute(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Minute
        /// </summary>
        public static DateTime LastTimeOfMinute(this DateTime value)
        {
            return value.FirstTimeOfMinute().AddMinutes(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Second
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfSecond(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, 0);
        }

        /// <summary>
        /// Last Millisecond Of Second
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfSecond(this DateTime value)
        {
            return value.FirstTimeOfSecond().AddSeconds(1).AddMilliseconds(-1);
        }

        #endregion Date Range


        #region Locale & Culture

        /// <summary>
        /// Get Month Text
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMonthString(this DateTime value)
        {
            byte index = (byte)Convert.ToDateTime(value).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var month = info.MonthNames[index];
            return month;
        }
        /// <summary>
        /// Get Month Text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetMonthString(this DateTime value, string location)
        {
            byte index = (byte)Convert.ToDateTime(value).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var month = info.MonthNames[index];
            return month;
        }

        /// <summary>
        /// Get Week Text
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(this DateTime value)
        {
            byte index = (byte)Convert.ToDateTime(value).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }
        /// <summary>
        /// Get Week Text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(this DateTime value, string location)
        {
            byte index = (byte)Convert.ToDateTime(value).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }

        #endregion Locale & Culture


        #region ROC

        /// <summary>
        /// To ROC Date String
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToROCDateString(this DateTime value, string format)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return value.ToString(format, culture);
        }

        /// <summary>
        /// Parse ROC Date String
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, out DateTime date)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// Parse ROC Date String
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, string format, out DateTime date)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (DateTime.TryParseExact(rocDateString, format, culture, DateTimeStyles.None, out date))
            {
                return true;
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// Parse ROC Date String
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="formats"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, IEnumerable<string> formats, out DateTime date)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (formats != null && formats.ToArray().Length != 0)
            {
                if (DateTime.TryParseExact(rocDateString, formats.ToArray(), culture, DateTimeStyles.None, out date))
                {
                    return true;
                }
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }

        #endregion
    }
}
