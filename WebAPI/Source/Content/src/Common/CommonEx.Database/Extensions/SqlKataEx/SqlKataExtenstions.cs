using SqlKata;

namespace CommonEx.Database.Extensions.SqlKataEx
{
    public static class SqlKataExtenstions
    {
        #region Select Columns

        /// <summary>
        /// Select All Columns
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Query SelectAllColumns(this Query query)
        {
            return query.Select("*");
        }

        /// <summary>
        /// Select Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectColumn(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return query.Select(column.ToRaw());
        }

        /// <summary>
        /// Select Columns
        /// </summary>
        /// <param name="query"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static Query SelectColumns(this Query query, IEnumerable<ColumnQuery> columns)
        {
            if (columns == null)
            {
                return query;
            }

            string[] columnRaws = columns.Select(c => c.ToRaw()).ToArray();

            return query.Select(columnRaws);
        }

        #endregion


        #region Select With Aggregate Function

        /// <summary>
        /// Select Max
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectMax(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return SelectByAggregateFunction(query, "Max", column);
        }

        /// <summary>
        /// Select Min
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectMin(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return SelectByAggregateFunction(query, "Min", column);
        }

        /// <summary>
        /// Select Count
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectCount(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return SelectByAggregateFunction(query, "Count", column);
        }

        /// <summary>
        /// Select Sum
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectSum(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return SelectByAggregateFunction(query, "Sum", column);
        }

        /// <summary>
        /// Select Sum
        /// </summary>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Query SelectAvg(this Query query, ColumnQuery column)
        {
            if (column == null)
            {
                return query;
            }
            return SelectByAggregateFunction(query, "Avg", column);
        }

        /// <summary>
        /// Select By Aggregate Function
        /// </summary>
        /// <param name="query"></param>
        /// <param name="function"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private static Query SelectByAggregateFunction(Query query, string function, ColumnQuery column)
        {
            string columnName = column.ColumnName;
            string tableName = column.TableName;
            string schemaName = column.SchemaName;
            string aliasName = column.AliasName;

            if (string.IsNullOrWhiteSpace(columnName))
            {
                return query;
            }

            string raw = $"[{columnName}]";

            if (!string.IsNullOrWhiteSpace(tableName))
            {
                if (!string.IsNullOrWhiteSpace(schemaName))
                {
                    raw = $"[{schemaName}].[{tableName}].{raw}";
                }
                else
                {
                    raw = $"[{tableName}].{raw}";
                }
            }

            raw = $"{function}({raw})";

            if (!string.IsNullOrWhiteSpace(aliasName))
            {
                raw = $"{raw} AS [{aliasName}]";
            }
            return query.SelectRaw(raw);
        }

        #endregion


        #region Where & Order

        /// <summary>
        /// And Where Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereCondiction"></param>
        /// <returns></returns>
        public static Query WhereColumn(this Query query, WhereCondiction whereCondiction)
        {
            if (whereCondiction == null)
            {
                return query;
            }

            // 檢查是否為 Sub Where
            if (whereCondiction.SubWhere != null &&
                whereCondiction.SubWhere.Any())
            {
                return query.Where(q => q.WhereColumns(whereCondiction.SubWhere));
            }

            // 檢查是否有 Operation
            if (string.IsNullOrEmpty(whereCondiction.Operation))
            {   // Equals
                return query.Where(whereCondiction.Column, whereCondiction.Value);
            }

            // 處理 Operation
            switch (whereCondiction.Operation.ToUpper())
            {
                case WhereOperation.Null:
                    return query.WhereNull(whereCondiction.Column);

                case WhereOperation.Like:
                    return query.WhereLike(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Starts:
                    return query.WhereStarts(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Ends:
                    return query.WhereEnds(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Contains:
                    return query.WhereContains(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                default:
                    return query.Where(whereCondiction.Column, whereCondiction.Operation, whereCondiction.Value);
            }
        }

        /// <summary>
        /// And Where Column Not
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereCondiction"></param>
        /// <returns></returns>
        public static Query WhereColumnNot(this Query query, WhereCondiction whereCondiction)
        {
            if (whereCondiction == null)
            {
                return query;
            }

            // 檢查是否為 Sub Where
            if (whereCondiction.SubWhere != null &&
                whereCondiction.SubWhere.Any())
            {
                return query.WhereNot(q => q.WhereColumns(whereCondiction.SubWhere));
            }

            // 檢查是否有 Operation
            if (string.IsNullOrEmpty(whereCondiction.Operation))
            {   // Equals
                return query.WhereNot(whereCondiction.Column, whereCondiction.Value);
            }

            // 處理 Operation
            switch (whereCondiction.Operation.ToUpper())
            {
                case WhereOperation.Null:
                    return query.WhereNotNull(whereCondiction.Column);

                case WhereOperation.Like:
                    return query.WhereNotLike(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Starts:
                    return query.WhereNotStarts(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Ends:
                    return query.WhereNotEnds(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Contains:
                    return query.WhereNotContains(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                default:
                    return query.WhereNot(whereCondiction.Column, whereCondiction.Operation, whereCondiction.Value);
            }
        }

        /// <summary>
        /// Or Where Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereCondiction"></param>
        /// <returns></returns>
        public static Query OrWhereColumn(this Query query, WhereCondiction whereCondiction)
        {
            if (whereCondiction == null)
            {
                return query;
            }

            // 檢查是否為 Sub Where
            if (whereCondiction.SubWhere != null &&
                whereCondiction.SubWhere.Any())
            {
                return query.OrWhere(q => q.WhereColumns(whereCondiction.SubWhere));
            }

            // 檢查是否有 Operation
            if (string.IsNullOrEmpty(whereCondiction.Operation))
            {   // Equals
                return query.OrWhere(whereCondiction.Column, whereCondiction.Value);
            }

            // 處理 Operation
            switch (whereCondiction.Operation.ToUpper())
            {
                case WhereOperation.Null:
                    return query.OrWhereNull(whereCondiction.Column);

                case WhereOperation.Like:
                    return query.OrWhereLike(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Starts:
                    return query.OrWhereStarts(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Ends:
                    return query.OrWhereEnds(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Contains:
                    return query.OrWhereContains(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                default:
                    return query.OrWhere(whereCondiction.Column, whereCondiction.Operation, whereCondiction.Value);
            }
        }

        /// <summary>
        /// Or Where Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereCondiction"></param>
        /// <returns></returns>
        public static Query OrWhereColumnNot(this Query query, WhereCondiction whereCondiction)
        {
            if (whereCondiction == null)
            {
                return query;
            }

            // 檢查是否為 Sub Where
            if (whereCondiction.SubWhere != null &&
                whereCondiction.SubWhere.Any())
            {
                return query.OrWhereNot(q => q.WhereColumns(whereCondiction.SubWhere));
            }

            // 檢查是否有 Operation
            if (string.IsNullOrEmpty(whereCondiction.Operation))
            {   // Equals
                return query.OrWhereNot(whereCondiction.Column, whereCondiction.Value);
            }

            // 處理 Operation
            switch (whereCondiction.Operation.ToUpper())
            {
                case WhereOperation.Null:
                    return query.OrWhereNotNull(whereCondiction.Column);

                case WhereOperation.Like:
                    return query.OrWhereNotLike(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Starts:
                    return query.OrWhereNotStarts(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Ends:
                    return query.OrWhereNotEnds(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                case WhereOperation.Contains:
                    return query.OrWhereNotContains(whereCondiction.Column, whereCondiction.Value, whereCondiction.CaseSensitive);

                default:
                    return query.OrWhereNot(whereCondiction.Column, whereCondiction.Operation, whereCondiction.Value);
            }
        }

        /// <summary>
        /// Where Columns
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereCondictions"></param>
        /// <returns></returns>
        public static Query WhereColumns(this Query query, List<WhereCondiction> whereCondictions)
        {
            Query outQuery = query;

            foreach (WhereCondiction whereCondition in whereCondictions)
            {
                switch (whereCondition.WhereType)
                {
                    case WhereCondictionType.And:
                        outQuery = outQuery.WhereColumn(whereCondition);
                        break;

                    case WhereCondictionType.AndNot:
                        outQuery = outQuery.WhereColumnNot(whereCondition);
                        break;

                    case WhereCondictionType.Or:
                        outQuery = outQuery.OrWhereColumn(whereCondition);
                        break;

                    case WhereCondictionType.OrNot:
                        outQuery = outQuery.OrWhereColumnNot(whereCondition);
                        break;
                }
            }
            return outQuery;
        }

        /// <summary>
        /// Order By Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderByCondiction"></param>
        /// <returns></returns>
        public static Query OrderByColumns(this Query query, OrderByCondiction orderByCondiction)
        {
            if (orderByCondiction == null ||
                orderByCondiction.Columns == null ||
                !orderByCondiction.Columns.Any(c => !string.IsNullOrWhiteSpace(c)))
            {
                return query;
            }

            List<string> columns = orderByCondiction.Columns
                                                    .Where(c => !string.IsNullOrWhiteSpace(c))
                                                    .ToList();
            if (orderByCondiction.IsDesc)
            {
                query = query.OrderByDesc(columns.ToArray());
            }
            else
            {
                query = query.OrderBy(columns.ToArray());
            }
            return query;
        }

        /// <summary>
        /// Order By Column
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderByCondictions"></param>
        /// <returns></returns>
        public static Query OrderByColumns(this Query query, List<OrderByCondiction> orderByCondictions)
        {
            if (orderByCondictions != null &&
                orderByCondictions.Any())
            {
                foreach (var orderByCondition in orderByCondictions)
                {
                    query = query.OrderByColumns(orderByCondition);
                }
            }
            return query;
        }

        #endregion
    }
}
