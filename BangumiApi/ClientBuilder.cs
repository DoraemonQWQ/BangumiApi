using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace BangumiApi
{
    /// <summary>
    /// Bangumi API客户端构建器
    /// </summary>
    public static class ClientBuilder
    {
        private const string DefaultBaseUrl = "https://api.bgm.tv";
        
        /// <summary>
        /// 创建一个无需认证的API客户端
        /// </summary>
        /// <param name="baseUrl">API基础URL，默认为"https://api.bgm.tv"</param>
        /// <returns>Bangumi API客户端</returns>
        public static ApiClient CreatePublicClient(string baseUrl = DefaultBaseUrl)
        {
            AnonymousAuthenticationProvider authProvider = new();
            HttpClientRequestAdapter adapter = new(authProvider)
            {
                BaseUrl = baseUrl,
            };
            return new ApiClient(adapter);
        }

        /// <summary>
        /// 创建一个带有AccessToken的API客户端
        /// </summary>
        /// <param name="authProvider">认证提供者 - 默认提供了一个BangumiAuthenticationProvider</param>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="baseUrl">API基础URL，默认为"https://api.bgm.tv"</param>
        /// <returns>Bangumi API客户端</returns>
        public static ApiClient CreateAuthenticatedClient(IAuthenticationProvider authProvider ,string accessToken, string baseUrl = DefaultBaseUrl)
        {
            // 创建一个简单的认证提供者，用于添加Bearer token
            HttpClientRequestAdapter adapter = new(authProvider)
            {
                BaseUrl = baseUrl,
            };
            return new ApiClient(adapter);
        }
    }
}