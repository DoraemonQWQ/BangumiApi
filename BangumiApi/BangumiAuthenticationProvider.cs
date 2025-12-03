using Microsoft.Kiota.Abstractions;
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;

namespace BangumiApi
{
    /// <summary>
    /// Bangumi API的认证提供者，用于在请求时添加AccessToken
    /// <param name="userAgent">User-Agent</param>
    /// <param name="accessAccessToken">访问令牌</param>
    /// </summary>
    public class BangumiAuthenticationProvider(string userAgent ,string accessAccessToken = "") : IAuthenticationProvider
    {

        private string _accessToken = accessAccessToken;
        
        /// <summary>
        /// 为请求添加认证头
        /// </summary>
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("User-Agent", userAgent);
            
            // 如果有AccessToken，则添加到Authorization头
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            }
            
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// 设置新的Token
        /// </summary>
        /// <param name="accessToken">新的accessToken</param>
        public void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }
    }
}