using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;

namespace CommonEx.Database.TypeHandlers
{
    /// <summary>
    /// Dapper JSON 轉換
    /// </summary>
    public class JsonTypeHandler : SqlMapper.ITypeHandler
    {
        /// <summary>
        /// Object to Database JSON Value
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = JToken.FromObject(value).ToString();
        }

        /// <summary>
        /// Database JSON Value to Object
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Parse(Type destinationType, object value)
        {
            if (value == null || value is not string)
            {
                return JToken.Parse("{}").ToObject(destinationType);
            }

            if (string.IsNullOrWhiteSpace(value as string))
            {
                return JToken.Parse("{}").ToObject(destinationType);
            }
            else
            {
                return JToken.Parse(value as string).ToObject(destinationType);
            }
        }
    }
}
