using CommonEx.Database.Extensions.SqlKataEx;

namespace CommonEx.Database.Repositories
{
    /// <summary>
    /// Sqlkata Query
    /// </summary>
    public class SqlQueryable
    {

        public List<string> Columns { get; set; } = new List<string>();

        public int Limit { get; set; } = 0;

        public int Offset { get; set; } = 0;

        public List<WhereCondiction> WhereCondictions { get; set; } = new List<WhereCondiction>();

        public List<OrderByCondiction> OrderByCondictions { get; set; } = new List<OrderByCondiction>();


        public SqlQueryable()
        {

        }
        public SqlQueryable(List<string> columns)
        {
            Columns = columns ?? new List<string>();
        }


        /// <summary>
        /// Query
        /// </summary>
        /// <returns></returns>
        public static SqlQueryable Query()
        {
            return new SqlQueryable();
        }
        /// <summary>
        /// Query
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static SqlQueryable Query(List<string> columns)
        {
            return new SqlQueryable(columns);
        }


        /// <summary>
        /// Select Columns
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlQueryable Select(params string[] columns)
        {
            Columns = new List<string>(columns);
            return this;
        }
        /// <summary>
        /// Select Columns
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlQueryable Select(List<string> columns)
        {
            Columns = columns ?? new List<string>();
            return this;
        }

        /// <summary>
        /// Set Limit
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public SqlQueryable Take(int limit)
        {
            Limit = limit;
            return this;
        }
        /// <summary>
        /// Set Offset
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public SqlQueryable Skip(int offset)
        {
            Offset = offset;
            return this;
        }
        /// <summary>
        /// Set Offset & Limit
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public SqlQueryable TakeAndSkip(int limit, int offset)
        {
            Offset = offset;
            Limit = limit;
            return this;
        }


        /// <summary>
        /// Add Where Condition
        /// </summary>
        /// <param name="whereCondiction"></param>
        /// <returns></returns>
        public SqlQueryable Where(WhereCondiction whereCondiction)
        {
            if (whereCondiction != null)
            {
                if (WhereCondictions == null)
                {
                    WhereCondictions = new List<WhereCondiction>();
                }
                WhereCondictions.Add(whereCondiction);
            }
            return this;
        }
        /// <summary>
        /// Add Where Conditions
        /// </summary>
        /// <param name="whereCondictions"></param>
        /// <returns></returns>
        public SqlQueryable Where(List<WhereCondiction> whereCondictions)
        {
            if (whereCondictions != null && whereCondictions.Any())
            {
                if (WhereCondictions == null)
                {
                    WhereCondictions = new List<WhereCondiction>();
                }
                WhereCondictions.AddRange(whereCondictions);
            }
            return this;
        }
        /// <summary>
        /// Add Where Conditions
        /// </summary>
        /// <param name="whereCondictions"></param>
        /// <returns></returns>
        public SqlQueryable Where(params WhereCondiction[] whereCondictions)
        {
            if (whereCondictions != null && whereCondictions.Any())
            {
                if (WhereCondictions == null)
                {
                    WhereCondictions = new List<WhereCondiction>();
                }
                WhereCondictions.AddRange(whereCondictions);
            }
            return this;
        }


        /// <summary>
        /// Add Order By Condition
        /// </summary>
        /// <param name="orderbyCondiction"></param>
        /// <returns></returns>
        public SqlQueryable OrderBy(OrderByCondiction orderbyCondiction)
        {
            if (orderbyCondiction != null)
            {
                if (OrderByCondictions == null)
                {
                    OrderByCondictions = new List<OrderByCondiction>();
                }
                OrderByCondictions.Add(orderbyCondiction);
            }
            return this;
        }
        /// <summary>
        /// Add Order By Conditions
        /// </summary>
        /// <param name="orderbyCondictions"></param>
        /// <returns></returns>
        public SqlQueryable OrderBy(List<OrderByCondiction> orderbyCondictions)
        {
            if (orderbyCondictions != null && orderbyCondictions.Any())
            {
                if (OrderByCondictions == null)
                {
                    OrderByCondictions = new List<OrderByCondiction>();
                }
                OrderByCondictions.AddRange(orderbyCondictions);
            }
            return this;
        }
    }
}
