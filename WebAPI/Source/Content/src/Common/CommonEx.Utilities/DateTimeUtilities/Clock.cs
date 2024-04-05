namespace CommonEx.Utilities.DateTimeUtilities
{
    public class Clock : IClock
    {
        /// <summary>
        /// Get Now
        /// </summary>
        public virtual DateTime Now
        {
            get
            {
                if (Kind == DateTimeKind.Utc)
                {
                    return DateTime.UtcNow;
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Get Today
        /// </summary>
        public virtual DateTime Today
        {
            get
            {
                if (Kind == DateTimeKind.Utc)
                {
                    return DateTime.UtcNow.Date;
                }
                else
                {
                    return DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Get kind
        /// </summary>
        public virtual DateTimeKind Kind { get; private set; } = DateTimeKind.Local;

        /// <summary>
        /// Support Multiple Timezone
        /// </summary>
        public virtual bool SupportsMultipleTimezone
        {
            get
            {
                return (Kind == DateTimeKind.Utc);
            }
        }

        public Clock()
        {
            Kind = DateTimeKind.Local;
        }
        public Clock(DateTimeKind kind)
        {
            Kind = kind;
        }

        /// <summary>
        /// Normalize
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public virtual DateTime Normalize(DateTime dateTime)
        {
            if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind)
            {
                return dateTime;
            }

            if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return DateTime.SpecifyKind(dateTime, Kind);
        }
    }
}
