using Serilog.Events;
using Serilog.Formatting;
using System.Net; // Necesario para WebUtility
using System.Text;

namespace Kash.FicheroLog.Formatters;

/// <summary>
/// Formateador que genera SOLAMENTE la entrada individual del log (el DIV).
/// La cabecera y estructura global se manejan en HtmlLogHooks.
/// </summary>
public sealed class HtmlLogFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        WriteLogEntry(logEvent, output);
    }

    private void WriteLogEntry(LogEvent logEvent, TextWriter output)
    {
        var level = logEvent.Level.ToString().ToLowerInvariant();
        var timestamp = logEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var message = WebUtility.HtmlEncode(logEvent.RenderMessage());
        var levelIcon = GetLevelIcon(logEvent.Level);

        var html = new StringBuilder();

        // Inicio de la tarjeta del log
        html.AppendLine($"<div class='log-entry {level}' data-level='{level}'>");

        // Header del log (Icono, fecha, nivel)
        html.AppendLine($"  <div class='log-header'>");
        html.AppendLine($"    <span class='timestamp'>{levelIcon} {timestamp}</span>");
        html.AppendLine($"    <span class='level'>{logEvent.Level}</span>");
        html.AppendLine($"  </div>");

        // Cuerpo del mensaje
        html.AppendLine($"  <div class='log-body'>");
        html.AppendLine($"    <div class='log-message'>{message}</div>");

        // Propiedades (Variables estructuradas)
        if (logEvent.Properties.Any())
        {
            html.AppendLine($"    <div class='log-properties'>");
            html.AppendLine($"      <strong>📦 Propiedades:</strong>");

            foreach (var property in logEvent.Properties)
            {
                var propertyName = WebUtility.HtmlEncode(property.Key);
                // Limpiamos comillas extra que a veces deja Serilog
                var propertyValue = WebUtility.HtmlEncode(property.Value.ToString().Trim('"'));

                html.AppendLine($"      <div class='property'>");
                html.AppendLine($"        <span class='property-name'>{propertyName}:</span>");
                html.AppendLine($"        <span class='property-value'>{propertyValue}</span>");
                html.AppendLine($"      </div>");
            }
            html.AppendLine($"    </div>");
        }

        // Excepciones (Errores detallados)
        if (logEvent.Exception != null)
        {
            var exceptionText = WebUtility.HtmlEncode(logEvent.Exception.ToString());
            html.AppendLine($"    <div class='exception'>");
            html.AppendLine($"      <strong>💣 Excepción:</strong><br><br>");
            html.AppendLine($"      {exceptionText}");
            html.AppendLine($"    </div>");
        }

        html.AppendLine($"  </div>"); // Cierre log-body
        html.AppendLine($"</div>");   // Cierre log-entry
        html.AppendLine();            // Salto de línea para legibilidad en el archivo raw

        output.Write(html.ToString());
    }

    private static string GetLevelIcon(LogEventLevel level)
    {
        return level switch
        {
            LogEventLevel.Error or LogEventLevel.Fatal => "❌",
            LogEventLevel.Warning => "⚠️",
            LogEventLevel.Information => "ℹ️",
            LogEventLevel.Debug => "🐛",
            LogEventLevel.Verbose => "🔍",
            _ => "📝"
        };
    }
}