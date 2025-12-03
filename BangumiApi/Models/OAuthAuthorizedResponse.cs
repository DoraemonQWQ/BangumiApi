using System.Text.Json.Serialization;

namespace BangumiApi.Models;

public record OAuthAuthorizedResponse()
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = "";
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = "";
    
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = "";
    
    [JsonPropertyName("scope")]
    public string Scope { get; init; } = "";
    
    [JsonPropertyName("user_id")]
    public int UserId { get; init; }

}
