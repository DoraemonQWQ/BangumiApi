using Microsoft.Kiota.Abstractions;
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;

namespace BangumiApi
{
    /// <summary>
    /// Bangumi API的认证提供者，用于在请求时添加AccessToken
    /// </summary>
    public class BangumiAuthenticationProvider(string userAgent ,string? accessToken = null) : IAuthenticationProvider
    {

        /// <summary>
        /// 为请求添加认证头
        /// </summary>
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("User-Agent", userAgent);
            
            // 如果有AccessToken，则添加到Authorization头
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            
            return Task.CompletedTask;
        }
    }
}