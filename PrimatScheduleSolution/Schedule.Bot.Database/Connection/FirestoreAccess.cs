using System.Text.Json;
using System.Text.Json.Serialization;

namespace Schedule.Bot.Firestore.Connection;

public sealed class FirestoreAccess
{
    [JsonPropertyName("project_id")]
    public required string ProjectId { get; init; }

    [JsonPropertyName("private_key_id")]
    public required string PrivateKeyId { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
