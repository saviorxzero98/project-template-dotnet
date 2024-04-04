namespace CommonEx.Database.Extensions.SqlKataEx
{
    /// <summary>
    /// Sqlkata Where Condiction Type
    /// </summary>
    public enum WhereCondictionType
    {
        And = 0,
        Or = 1,
        AndNot = 2,
        OrNot = 3
    }

    /// <summary>
    /// Sqlkata Where Operation
    /// </summary>
    public class WhereOperation
    {
        public const string Like = "LIKE";

        public const string Null = "NULL";

        public const string Starts = "STARTS";

        public const string Ends = "ENDS";

        public const string Contains = "CONTAINS";
    }

    /// <summary>
    /// Sqlkata Where Condi
    /// </summary>
    public class WhereCondiction
    {
        #region Internal Flag

        /// <summary>
        /// Where類型，
        /// 0：AND,
        /// 1：OR,
        /// 2：AND NOT,
        /// 3：OR NOT
        /// </summary>
        internal WhereCondictionType WhereType { get; set; }

        /// <summary>
        /// 字串 LIKE 比對時，是否忽略大小寫，true：區分大小寫；false 不分大小寫
        /// </summary>
        internal bool CaseSensitive { get; set; }

        #endregion


        #region Property

        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 比對運算
        /// </summary>
        public string Operation { get; set; } = string.Empty;

        /// <summary>
        /// 欄位值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// SubWhere Condiction
        /// </summary>
        public List<WhereCondiction> SubWhere { get; set; } = new List<WhereCondiction>();

        #endregion


        #region Constructor

        public WhereCondiction(WhereCondictionType type, string column, object value)
        {
            Column = column;
            Operation = string.Empty;
            Value = value;
            WhereType = type;
            CaseSensitive = true;
        }
        public WhereCondiction(WhereCondictionType type, string column, string op, object value)
        {
            Column = column;
            Operation = op;
            Value = value;
            WhereType = type;
            CaseSensitive = true;
        }
        public WhereCondiction(WhereCondictionType type, List<WhereCondiction> subWhere)
        {
            Column = string.Empty;
            Operation = string.Empty;
            SubWhere = subWhere ?? new List<WhereCondiction>();
            WhereType = type;
            CaseSensitive = true;
        }

        #endregion


        #region And Where

        /// <summary>
        /// And Where
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction Where(string column, object value)
        {
            return new WhereCondiction(WhereCondictionType.And, column, value);
        }
        /// <summary>
        /// And Where
        /// </summary>
        /// <param name="column"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction Where(string column, string op, object value)
        {
            return new WhereCondiction(WhereCondictionType.And, column, op, value);
        }
        /// <summary>
        /// And Where
        /// </summary>
        /// <param name="subWhere"></param>
        /// <returns></returns>
        public static WhereCondiction Where(List<WhereCondiction> subWhere)
        {
            return new WhereCondiction(WhereCondictionType.And, subWhere);
        }
        /// <summary>
        /// And Where Like
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="compareType"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static WhereCondiction WhereString(string column, string value,
                                                  string compareType = WhereOperation.Like,
                                                  bool ignoreCase = false)
        {
            string op = WhereOperation.Like;
            switch (compareType.ToUpper())
            {
                case WhereOperation.Starts:
                case WhereOperation.Ends:
                case WhereOperation.Contains:
                    op = compareType.ToUpper();
                    break;
            }

            return new WhereCondiction(WhereCondictionType.And, column, op, value)
            {
                CaseSensitive = !ignoreCase
            };
        }
        /// <summary>
        /// And Where Null
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNull(string column)
        {
            return new WhereCondiction(WhereCondictionType.And, column, WhereOperation.Null, null);
        }


        /// <summary>
        /// And Where Not
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNot(string column, object value)
        {
            return new WhereCondiction(WhereCondictionType.AndNot, column, value);
        }
        /// <summary>
        /// And Where Not
        /// </summary>
        /// <param name="column"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNot(string column, string op, object value)
        {
            return new WhereCondiction(WhereCondictionType.AndNot, column, op, value);
        }
        /// <summary>
        /// And Where Not
        /// </summary>
        /// <param name="subWhere"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNot(List<WhereCondiction> subWhere)
        {
            return new WhereCondiction(WhereCondictionType.AndNot, subWhere);
        }
        /// <summary>
        /// And Where Not Like
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="compareType"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNotString(string column, string value,
                                                     string compareType = WhereOperation.Like,
                                                     bool ignoreCase = false)
        {
            string op = WhereOperation.Like;
            switch (compareType.ToUpper())
            {
                case WhereOperation.Starts:
                case WhereOperation.Ends:
                case WhereOperation.Contains:
                    op = compareType.ToUpper();
                    break;
            }

            return new WhereCondiction(WhereCondictionType.AndNot, column, op, value)
            {
                CaseSensitive = !ignoreCase
            };
        }
        /// <summary>
        /// And Where Not Null
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static WhereCondiction WhereNotNull(string column)
        {
            return new WhereCondiction(WhereCondictionType.AndNot, column, WhereOperation.Null, null);
        }

        #endregion


        #region Or Where

        /// <summary>
        /// Or Where
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhere(string column, object value)
        {
            return new WhereCondiction(WhereCondictionType.Or, column, value);
        }
        /// <summary>
        /// Or Where
        /// </summary>
        /// <param name="column"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhere(string column, string op, object value)
        {
            return new WhereCondiction(WhereCondictionType.Or, column, op, value);
        }
        /// <summary>
        /// Or Where
        /// </summary>
        /// <param name="subWhere"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhere(List<WhereCondiction> subWhere)
        {
            return new WhereCondiction(WhereCondictionType.Or, subWhere);
        }
        /// <summary>
        /// Or Where Like
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="compareType"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereString(string column, string value,
                                                    string compareType = WhereOperation.Like,
                                                    bool ignoreCase = false)
        {
            string op = WhereOperation.Like;
            switch (compareType.ToUpper())
            {
                case WhereOperation.Starts:
                case WhereOperation.Ends:
                case WhereOperation.Contains:
                    op = compareType.ToUpper();
                    break;
            }

            return new WhereCondiction(WhereCondictionType.Or, column, op, value)
            {
                CaseSensitive = !ignoreCase
            };
        }
        /// <summary>
        /// Or Where Null
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNull(string column)
        {
            return new WhereCondiction(WhereCondictionType.Or, column, WhereOperation.Null, null);
        }


        /// <summary>
        /// Or Where Not
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNot(string column, object value)
        {
            return new WhereCondiction(WhereCondictionType.OrNot, column, value);
        }
        /// <summary>
        /// Or Where Not
        /// </summary>
        /// <param name="column"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNot(string column, string op, object value)
        {
            return new WhereCondiction(WhereCondictionType.OrNot, column, op, value);
        }
        /// <summary>
        /// Or Where Not
        /// </summary>
        /// <param name="subWhere"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNot(List<WhereCondiction> subWhere)
        {
            return new WhereCondiction(WhereCondictionType.OrNot, subWhere);
        }
        /// <summary>
        /// Or Where Not Like
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="compareType"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNotString(string column, string value,
                                                       string compareType = WhereOperation.Like,
                                                       bool ignoreCase = false)
        {
            string op = WhereOperation.Like;
            switch (compareType.ToUpper())
            {
                case WhereOperation.Starts:
                case WhereOperation.Ends:
                case WhereOperation.Contains:
                    op = compareType.ToUpper();
                    break;
            }

            return new WhereCondiction(WhereCondictionType.OrNot, column, op, value)
            {
                CaseSensitive = !ignoreCase
            };
        }
        /// <summary>
        /// Or Where Not Null
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static WhereCondiction OrWhereNotNull(string column)
        {
            return new WhereCondiction(WhereCondictionType.OrNot, column, WhereOperation.Null, null);
        }

        #endregion
    }
}
