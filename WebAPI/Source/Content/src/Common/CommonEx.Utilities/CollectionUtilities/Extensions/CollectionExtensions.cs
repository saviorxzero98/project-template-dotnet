using System.Collections;

namespace CommonEx.Utilities.CollectionUtilities.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Array Count
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Count(this Array value)
        {
            return value.Length;
        }

        /// <summary>
        /// Is Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this ICollection value)
        {
            return (value == null);
        }

        /// <summary>
        /// Is Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this ICollection value)
        {
            return (value.Count == 0);
        }

        /// <summary>
        /// Is Null Or Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this ICollection value)
        {
            return (value == null || value.Count == 0);
        }
    }
}
