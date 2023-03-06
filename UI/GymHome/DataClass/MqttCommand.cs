using System.Text.Json.Serialization;

namespace GymHome
{
    public class MqttCommand
    {
        [JsonPropertyName("comando")]
        public string Command { get; set; }

        [JsonPropertyName("opcao")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Arg { get; set; }
    }
}
