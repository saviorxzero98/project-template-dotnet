namespace CommonEx.HttpClients
{
    public class WebApiClientSettings
    {
        /// <summary>
        /// 對應 appsettings.json 的屬性
        /// </summary>
        public const string SettingName = "HttpClient";

        /// <summary>
        /// 錯誤重試次數預設值
        /// </summary>
        internal const int DefaultRetryCount = 3;

        /// <summary>
        /// 重試間隔秒數預設值
        /// </summary>
        internal const int DefaultRetryBaseSeconds = 3;

        /// <summary>
        /// Http請求逾時預設秒數
        /// </summary>
        internal const long DefaultTimeoutInSeconds = 100;


        /// <summary>
        /// 是否檢查 HTTPS 憑證
        /// </summary>
        public bool CertificateValidation { get; set; } = true;

        /// <summary>
        /// 錯誤重試次數
        /// </summary>
        public int RetryCount { get; set; } = DefaultRetryCount;

        /// <summary>
        /// 重試間隔秒數
        /// </summary>
        public double RetryBaseSeconds { get; set; } = DefaultRetryBaseSeconds;

        /// <summary>
        /// Http請求逾時秒數
        /// </summary>
        public long TimeoutInSeconds { get; set; } = DefaultTimeoutInSeconds;
    }
}
