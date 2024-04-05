namespace CommonEx.Utilities.DateTimeUtilities
{
    public interface IClock
    {
        /// <summary>
        /// Get Now
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Get Today
        /// </summary>
        DateTime Today { get; }

        /// <summary>
        /// Get kind
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// Support Multiple Timezone
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// Normalize
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DateTime Normalize(DateTime dateTime);
    }
}
