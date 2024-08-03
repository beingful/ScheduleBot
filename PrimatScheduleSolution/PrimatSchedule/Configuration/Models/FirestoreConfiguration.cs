using System.Text.Json.Serialization;

namespace PrimatScheduleBot.Configuration.Models;

public sealed class FirestoreConfiguration : IConfigurationSection
{
    [JsonPropertyName("project_id")]
    public required string ProjectId { get; init; }

    [JsonPropertyName("private_key_id")]
    public required string PrivateKeyId { get; init; }
}
