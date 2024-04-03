using System.Data;

namespace CommonEx.Utilities.CollectionUtilities.Extensions
{
    public static class DataTableExtensions
    {
        #region Data Column Extensions

        /// <summary>
        /// Contain Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ContainColumn(this DataTable table, string name)
        {
            return table.Columns.Contains(name);
        }

        /// <summary>
        /// Column Count
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int ColumnCount(this DataTable table)
        {
            return table.Columns.Count;
        }

        /// <summary>
        /// Add Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable AddColumn(this DataTable table, string name, Type type)
        {
            if (!table.Columns.Contains(name))
            {
                table.Columns.Add(name, type);
            }

            return table;
        }

        /// <summary>
        /// Add Column (with Expression)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataTable AddColumn(this DataTable table, string name, Type type, string expression)
        {
            if (!table.Columns.Contains(name))
            {
                table.Columns.Add(name, type, expression);
            }

            return table;
        }

        /// <summary>
        /// Add Column (with Function)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static DataTable AddColumn(this DataTable table, string name, Type type, Func<DataRow, object> predicate)
        {
            // Add Column
            if (!table.Columns.Contains(name) && predicate != null)
            {
                table.Columns.Add(name, type);
            }

            // Assign Value
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                row[name] = predicate(row);
            }

            return table;
        }

        /// <summary>
        /// Update Column (with Expression)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataTable UpdateColumn(this DataTable table, string name, Type type, string expression)
        {
            // Temp Column Name
            string calcColumnName = GetCalcColumn(name);
            string tempColumnName = GetTempColumn(name);
            int columnOrdinal = table.Columns[name].Ordinal;

            // Add Calc
            table.AddColumn(calcColumnName, type, expression)
                 .CopyColumn(calcColumnName, tempColumnName)
                 .RemoveColumn(calcColumnName)
                 .RemoveColumn(name)
                 .RenameColumn(tempColumnName, name)
                 .SetColumnOrdinal(name, columnOrdinal);

            return table;
        }

        /// <summary>
        /// Update Column (with Function)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static DataTable UpdateColumn(this DataTable table, string name, Type type, Func<DataRow, object> predicate)
        {
            // Check Column Name
            if (!table.ContainColumn(name))
            {
                return table;
            }

            string columnTempName = GetTempColumn(name);
            int columnOrdinal = table.Columns[name].Ordinal;

            // Add Temp Column
            table.AddColumn(columnTempName, type, predicate)
                 .RemoveColumn(name)
                 .RenameColumn(columnTempName, name)
                 .SetColumnOrdinal(name, columnOrdinal);

            return table;
        }


        /// <summary>
        /// Copy Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fromColumn"></param>
        /// <param name="toColumn"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DataTable CopyColumn(this DataTable table, string fromColumn, string toColumn, int ordinal = -1)
        {
            if (table.ContainColumn(fromColumn))
            {
                // Add Column
                table.Columns.Add(toColumn, table.Columns[fromColumn].DataType);

                // Ordinal Column
                if (ordinal >= 0 && ordinal < table.Columns.Count)
                {
                    table.Columns[toColumn].SetOrdinal(ordinal);
                }

                // Assign Value
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];

                    row[toColumn] = row[fromColumn];
                }
            }
            return table;
        }

        /// <summary>
        /// Rename Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oldColumn"></param>
        /// <param name="newColumn"></param>
        /// <returns></returns>
        public static DataTable RenameColumn(this DataTable table, string oldColumn, string newColumn)
        {
            if (table.ContainColumn(oldColumn))
            {
                table.Columns[oldColumn].ColumnName = newColumn;
            }

            return table;
        }

        /// <summary>
        /// Remove Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable RemoveColumn(this DataTable table, string name)
        {
            // Check Column Name
            if (table.ContainColumn(name))
            {
                // Delete Column Name
                table.Columns.Remove(name);
            }
            return table;
        }

        /// <summary>
        /// Set Column Ordinal
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="ordinal"></param>
        /// <returns></returns>
        public static DataTable SetColumnOrdinal(this DataTable table, string name, int ordinal = -1)
        {
            // Ordinal Column
            if (table.ContainColumn(name) &&
                ordinal >= 0 && ordinal < table.Columns.Count)
            {
                table.Columns[name].SetOrdinal(ordinal);
            }
            return table;
        }

        /// <summary>
        /// Select Data Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static DataTable SelectColumns(this DataTable table, params string[] columnNames)
        {
            return table.SelectDistinctData(false, columnNames);
        }

        #endregion


        #region Data Row Extensions

        ///// <summary>
        ///// Row Count
        ///// </summary>
        ///// <param name="table"></param>
        ///// <returns></returns>
        //public static int RowCount(this DataTable table)
        //{
        //    return table.Rows.Count;
        //}

        ///// <summary>
        ///// Take First N Data Rows
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static DataTable TakeTopRows(this DataTable table, int count)
        //{
        //    var outTable = table.Rows
        //                        .TakeTop(count)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Take First N Data Rows (With Function)
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public static DataTable TakeTopRows(this DataTable table, int count, Func<DataRow, bool> predicate)
        //{
        //    var outTable = table.Rows
        //                        .TakeTop(count, predicate)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Take Last N Data Rows
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static DataTable TakeLastRows(this DataTable table, int count)
        //{
        //    var outTable = table.Rows
        //                        .TakeLast(count)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Last Last N Data Rows (With Function)
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public static DataTable TakeLastRows(this DataTable table, int count, Func<DataRow, bool> predicate)
        //{
        //    var outTable = table.Rows
        //                        .TakeLast(count, predicate)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Skip First N Data Rows
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static DataTable SkipTopRows(this DataTable table, int count)
        //{
        //    var outTable = table.Rows
        //                        .SkipTop(count)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Skip First N Data Rows (With Function)
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public static DataTable SkipTopRows(this DataTable table, int count, Func<DataRow, bool> predicate)
        //{
        //    var outTable = table.Rows
        //                        .SkipTop(count, predicate)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Skip Last N Data Rows
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public static DataTable SkipLastRows(this DataTable table, int count)
        //{
        //    var outTable = table.Rows
        //                        .SkipLast(count)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        ///// <summary>
        ///// Skip Last N Data Rows (With Function)
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="count"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public static DataTable SkipLastRows(this DataTable table, int count, Func<DataRow, bool> predicate)
        //{
        //    var outTable = table.Rows
        //                        .SkipLast(count, predicate)
        //                        .CopyToDataTable();
        //    return outTable;
        //}

        /// <summary>
        /// Filter Data Rows
        /// </summary>
        /// <param name="table"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataTable FilterRows(this DataTable table, string expression)
        {
            var view = new DataView(table)
            {
                RowFilter = expression
            };
            var outTable = view.ToTable();
            return outTable;
        }

        /// <summary>
        /// Distinct Data Rows
        /// </summary>
        /// <param name="table"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public static DataTable DistinctRows(this DataTable table)
        {
            List<string> columns = table.Columns
                                        .GetColumnNames()
                                        .ToList();
            return table.SelectDistinctData(true, columns.ToArray());
        }

        #endregion


        #region Data

        /// <summary>
        /// Select Distinct Data
        /// </summary>
        /// <param name="table"></param>
        /// <param name="distinct"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static DataTable SelectDistinctData(this DataTable table, bool distinct, params string[] columnNames)
        {
            DataView view = new DataView(table);
            var outTable = view.ToTable(distinct, columnNames);
            return outTable;
        }

        #endregion


        #region Common

        /// <summary>
        /// Get Temp Column Name
        /// </summary>
        /// <param name="name"></param>
        private static string GetTempColumn(string name)
        {
            return $"$Temp_{name}";
        }

        /// <summary>
        /// Get Calc Column Name
        /// </summary>
        /// <param name="name"></param>
        private static string GetCalcColumn(string name)
        {
            return $"$Calc_{name}";
        }

        #endregion
    }
}
