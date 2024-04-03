using System.Data;

namespace CommonEx.Utilities.CollectionUtilities.Extensions
{
    public static class DataColumnCollectionExtensions
    {
        /// <summary>
        /// Get Column Names
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames(this DataColumnCollection columns)
        {
            var columnNames = columns.Cast<DataColumn>()
                                     .Select(c => c.ColumnName);
            return columnNames;
        }
    }
}
