using Microsoft.Extensions.Logging.Console;

namespace Quenee.ConsoleApp.ConsoleFormatters
{
    public class QueeneConsoleFormatterOptions : JsonConsoleFormatterOptions
    {
        public bool ExcludeNotes { get; set; }
    }
}
