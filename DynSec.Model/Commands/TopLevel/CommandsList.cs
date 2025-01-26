using DynSec.Model.Commands.Abstract;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace DynSec.Model.Commands.TopLevel
{
    public sealed class CommandsList
    {
        public CommandsList() { }
        public CommandsList(List<AbstractCommand> commands)
        {
            _commands = _commands.Concat(commands).ToList();
        }

        private readonly List<AbstractCommand> _commands = new();

        public List<AbstractCommand> Commands { get { return _commands; } }

        public string AsJSON()
        {
            var jsonoptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin),
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Serialize(this, jsonoptions);
        }

    }
}
