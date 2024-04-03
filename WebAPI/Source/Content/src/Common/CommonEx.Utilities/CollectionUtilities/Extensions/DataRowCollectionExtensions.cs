using System.Data;

namespace CommonEx.Utilities.CollectionUtilities.Extensions
{
    public static class DataRowCollectionExtensions
    {
        /// <summary>
        /// Take First N Rows
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> TakeTop(this DataRowCollection dataRow, int count)
        {
            var rows = dataRow.Cast<DataRow>()
                              .Take(count);
            return rows;
        }

        /// <summary>
        /// Take First N Row (with Function)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> TakeTop(this DataRowCollection dataRow, int count,
                                                   Func<DataRow, bool> predicate)
        {
            var rows = dataRow.Cast<DataRow>()
                              .TakeWhile(predicate)
                              .Take(count);
            return rows;
        }

        /// <summary>
        /// Take Last N Rows
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> TakeLast(this DataRowCollection dataRow, int count)
        {
            var rows = dataRow.Cast<DataRow>()
                              .Reverse()
                              .Take(count)
                              .Reverse();
            return rows;
        }

        /// <summary>
        /// Take Last N Row (with Function)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> TakeLast(this DataRowCollection dataRow, int count,
                                                    Func<DataRow, bool> predicate)
        {
            var rows = dataRow.Cast<DataRow>()
                              .TakeWhile(predicate)
                              .Reverse()
                              .Take(count)
                              .Reverse();
            return rows;
        }

        /// <summary>
        /// Skip First N Rows
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> SkipTop(this DataRowCollection dataRow, int count)
        {
            var rows = dataRow.Cast<DataRow>()
                              .Skip(count);
            return rows;
        }

        /// <summary>
        /// Skip First N Rows (Function)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> SkipTop(this DataRowCollection dataRow, int count,
                                                   Func<DataRow, bool> predicate)
        {
            var rows = dataRow.Cast<DataRow>()
                              .SkipWhile(predicate)
                              .Skip(count);
            return rows;
        }

        /// <summary>
        /// Skip Last N Rows
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> SkipLast(this DataRowCollection dataRow, int count)
        {
            var rows = dataRow.Cast<DataRow>()
                              .Reverse()
                              .Skip(count)
                              .Reverse();
            return rows;
        }

        /// <summary>
        /// Skip Last N Rows (with Function)
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="count"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> SkipLast(this DataRowCollection dataRow, int count,
                                                    Func<DataRow, bool> predicate)
        {
            var rows = dataRow.Cast<DataRow>()
                              .SkipWhile(predicate)
                              .Reverse()
                              .Skip(count)
                              .Reverse();
            return rows;
        }
    }
}
