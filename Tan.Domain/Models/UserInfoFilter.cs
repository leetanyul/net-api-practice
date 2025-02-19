using System.Text.Json.Serialization;

namespace Tan.Domain.Models;

public class UserInfoFilter
{
    [JsonPropertyName("timestamp")]
    public long TimeStamp { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("desc")]
    public string Desc { get; set; }

    [JsonPropertyName("content")]
    public AcoountFilter Account { get; set; }
}

public class AcoountFilter
{
    [JsonPropertyName("accountId")]
    public string AccountId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("subKey")]
    public List<AccountSubkeyFilter> SubKey { get; set; }
}

public class AccountSubkeyFilter
{
    [JsonPropertyName("subKey")]
    public string SubKey { get; set; }

    [JsonPropertyName("grade")]
    public string Grade { get; set; }
}