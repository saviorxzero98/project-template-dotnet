namespace CommonEx.Database.Extensions.SqlKataEx
{
    /// <summary>
    /// Sqlkata Order By Condition
    /// </summary>
    public class OrderByCondiction
    {
        /// <summary>
        /// Is DESC
        /// </summary>
        public bool IsDesc { get; set; } = false;

        /// <summary>
        /// Column
        /// </summary>
        public List<string> Columns { get; set; } = new List<string>();


        public OrderByCondiction()
        {

        }
        public OrderByCondiction(bool isDesc, List<string> columns)
        {
            IsDesc = isDesc;

            if (columns != null)
            {
                Columns = new List<string>(columns);
                Columns = Columns.Where(c => !string.IsNullOrWhiteSpace(c))
                                 .ToList();
            }
            else
            {
                Columns = new List<string>();
            }
        }
        public OrderByCondiction(bool isDesc, params string[] columns)
        {
            IsDesc = isDesc;

            if (columns != null)
            {
                Columns = new List<string>(columns);
                Columns = Columns.Where(c => !string.IsNullOrWhiteSpace(c))
                                 .ToList();
            }
            else
            {
                Columns = new List<string>();
            }
        }

        public static OrderByCondiction Asc(params string[] columns)
        {
            return new OrderByCondiction(false, columns);
        }
        public static OrderByCondiction Asc(List<string> columns)
        {
            return new OrderByCondiction(false, columns);
        }
        public static OrderByCondiction Desc(params string[] columns)
        {
            return new OrderByCondiction(true, columns);
        }
        public static OrderByCondiction Desc(List<string> columns)
        {
            return new OrderByCondiction(true, columns);
        }
    }
}
