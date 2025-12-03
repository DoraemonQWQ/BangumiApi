using System.Text.Json;
using BangumiApi.Models;

namespace BangumiApi;

/// <summary>
/// 获取Bangumi的API访问令牌
/// </summary>
public static class BangumiAuthenticationTokenAccess
{
    private static readonly HttpClient HttpClient = new HttpClient();
    
    /// <summary>
    /// 生成OAuth授权请求URL
    /// </summary>
    /// <param name="clientId">客户端ID</param>
    /// <param name="redirectUri">回调地址</param>
    /// <param name="state">状态数据</param>
    /// <returns>OAuth授权请求URL</returns>
    public static string InitiateOAuthAuthorizationRequest(string clientId, string redirectUri, string state)
    {
        return $"https://bgm.tv/oauth/authorize?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&state={Uri.EscapeDataString(state)}";
    }

    /// <summary>
    /// 获取OAuth授权数据
    /// </summary>
    /// <param name="userAgent">User-Agent</param>
    /// <param name="clientId">客户端ID</param>
    /// <param name="clientSecret">客户端密钥</param>
    /// <param name="redirectUri">回调路径</param>
    /// <param name="authorizationCode">授权码</param>
    public static async Task<OAuthAuthorizedResponse?> GetOAuthAuthorizedDataAsync(string userAgent,string clientId, string clientSecret, string redirectUri, string authorizationCode)
    { 
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://bgm.tv/oauth/token");
        request.Headers.Add("User-Agent", userAgent);
        request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
            ["redirect_uri"] = redirectUri,
            ["code"] = authorizationCode,
            ["grant_type"] = "authorization_code",
        });
        using HttpResponseMessage response = await HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<OAuthAuthorizedResponse>(json) ?? null;
    }
    
    /// <summary>
    /// 刷新OAuth访问令牌
    /// </summary>
    /// <param name="userAgent">User-Agent</param>
    /// <param name="clientId">客户端ID</param>
    /// <param name="clientSecret">客户端密钥</param>
    /// <param name="refreshToken">刷新令牌</param>
    public static async Task<OAuthAuthorizedResponse?> RefreshOAuthTokenAsync(string userAgent,string clientId, string clientSecret, string refreshToken)
    { 
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://bgm.tv/oauth/token");
        
        request.Headers.Add("User-Agent", userAgent);
        request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
            ["refresh_token"] = refreshToken,
            ["grant_type"] = "refresh_token",
        });
        using HttpResponseMessage response = await HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<OAuthAuthorizedResponse>(json) ?? null;
    }
}
