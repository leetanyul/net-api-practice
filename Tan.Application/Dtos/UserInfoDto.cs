using System.Text.Json.Serialization;

namespace Tan.Application.Dtos;

public record UserInfoDto
{
    [JsonPropertyName("timestamp")]
    public long TimeStamp { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("desc")]
    public string Desc { get; set; }

    [JsonPropertyName("content")]
    public AcoountDto Account { get; set; }
}

public record AcoountDto
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
    public string role { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("subKey")]
    public List<AccountSubkeyDto> SubKey { get; set; }
}

public record AccountSubkeyDto
{
    [JsonPropertyName("subKey")]
    public string SubKey { get; set; }

    [JsonPropertyName("grade")]
    public string Grade { get; set; }
}