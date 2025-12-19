using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System.IO;

namespace Quenee.ConsoleApp.ConsoleFormatters
{
    public class QueeneConsoleFormatter : ConsoleFormatter
    {
        public const string FormatterName = "QueeneConsoleFormatter";
        public const string TextColor = "\x1B[1m\x1B[37m";
        public const string TextNoteColor = "\x1B[32m";

        public QueeneConsoleFormatter(IOptionsMonitor<QueeneConsoleFormatterOptions> options) : base(FormatterName)
        {
            FormatterOptions = options.CurrentValue;
        }

        public QueeneConsoleFormatterOptions FormatterOptions { get; set; }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
        {
            string message = logEntry.Formatter(logEntry.State, logEntry.Exception);

            if (logEntry.Exception == null && string.IsNullOrWhiteSpace(message))
                return;

            if (FormatterOptions.ExcludeNotes == false)
            {
                textWriter.Write(TextNoteColor);
                textWriter.Write("Queene: ");
            }

            textWriter.Write(TextColor);
            textWriter.WriteLine(message);
        }
    }
}
