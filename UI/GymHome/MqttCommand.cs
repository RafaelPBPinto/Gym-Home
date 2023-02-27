using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
