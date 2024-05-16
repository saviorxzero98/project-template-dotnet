﻿namespace CommonEx.HttpClients
{
    public class HttpApiSettings
    {
        public const string SettingName = "HttpApiSetting";

        public const string HttpClientName = "HttpApiClient";

        /// <summary>
        /// Host Url
        /// </summary>
        public string HostUrl { get; set; } = string.Empty;

        /// <summary>
        /// 是否檢查 HTTPS 憑證
        /// </summary>
        public bool CertificateValidation { get; set; } = false;

        /// <summary>
        /// 錯誤重試次數
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// 重試間隔時間
        /// </summary>
        public long RetryWaitMilliseconds { get; set; } = 0;

        /// <summary>
        /// Request Timeout 時間
        /// </summary>
        public long TimeoutSeconds { get; set; } = 0;
    }
}
