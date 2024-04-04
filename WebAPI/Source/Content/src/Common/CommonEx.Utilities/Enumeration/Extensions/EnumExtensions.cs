using System.ComponentModel;

namespace CommonEx.Utilities.Enumeration.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get Name
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetName(this Enum self)
        {
            return self.ToString();
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetValue(this Enum self)
        {
            return Convert.ToInt32(self);
        }

        /// <summary>
        /// Get Description
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum self)
        {
            Type type = self.GetType();
            var name = Enum.GetNames(type)
                           .Where(f => f.Equals(self.ToString(), StringComparison.CurrentCultureIgnoreCase))
                           .Select(d => d)
                           .FirstOrDefault();

            // 找無相對應的列舉
            if (name == null)
            {
                return string.Empty;
            }

            // 利用反射找出相對應的欄位
            var field = type.GetField(name);
            // 取得欄位設定DescriptionAttribute的值
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return name;
            }

            // 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }

        /// <summary>
        /// Get Names
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetNames<TEnum>()
        {
            return Enum.GetNames(typeof(TEnum));
        }
        /// <summary>
        /// Get Values
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IEnumerable<int> GetValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<int>().ToList();
        }
        /// <summary>
        /// Get Descriptions
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetDescriptions<TEnum>()
        {
            return GetAllEnum<TEnum>().Select(e => (e as Enum).GetDescription());
        }
        /// <summary>
        /// Get All Enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetAllEnum<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultEnum"></param>
        /// <returns></returns>
        public static TEnum Parse<TEnum>(string value, TEnum defaultEnum = default(TEnum))
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch
            {
                return defaultEnum;
            }
        }
        /// <summary>
        /// Parse
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="defaultEnum"></param>
        /// <returns></returns>
        public static TEnum Parse<TEnum>(string value, bool ignoreCase, TEnum defaultEnum = default(TEnum))
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
            }
            catch
            {
                return defaultEnum;
            }
        }

        /// <summary>
        /// Parse Extensions
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultEnum"></param>
        /// <returns></returns>
        public static TEnum ParseEx<TEnum>(string value, TEnum defaultEnum = default(TEnum))
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch
            {
                var enums = GetAllEnum<TEnum>().ToList();

                foreach (var e in enums)
                {
                    if (value == (e as Enum).GetDescription())
                    {
                        return e;
                    }
                }
                return defaultEnum;
            }
        }
        /// <summary>
        /// Parse Extensions
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="defaultEnum"></param>
        /// <returns></returns>
        public static TEnum ParseEx<TEnum>(string value, bool ignoreCase, TEnum defaultEnum = default(TEnum))
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
            }
            catch
            {
                var enums = GetAllEnum<TEnum>().ToList();

                foreach (var e in enums)
                {
                    if (ignoreCase && value.ToUpper() == (e as Enum).GetDescription().ToUpper())
                    {
                        return e;
                    }
                    else if (value == (e as Enum).GetDescription())
                    {
                        return e;
                    }
                }
                return defaultEnum;
            }
        }
    }
}
