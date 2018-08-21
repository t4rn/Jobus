using System;

namespace Jobus.Core.Services.Loggers
{
    public class KrisLogger : ILogger
    {
        private readonly Logger _mainLogger;
        private readonly NLog.Logger _nLogMain;
        private readonly string _guid;

        public KrisLogger(SerilogSettings settings)
        {
            _mainLogger = PrepareMainLogger(settings);
            _nLogMain = NLog.LogManager.GetLogger("MAIN");
            _guid = Guid.NewGuid().ToString("N");
        }

        private Logger PrepareMainLogger(SerilogSettings settings)
        {
            LogEventLevel logLevel = Enum.Parse<LogEventLevel>(settings.LogLevel);

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()

                //.WriteTo.File(
                //    path: settings.FileDebugPath,
                //    outputTemplate: settings.OutputTemplate,
                //    restrictedToMinimumLevel: LogEventLevel.Debug,
                //    rollingInterval: RollingInterval.Day,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))

                //.WriteTo.File(
                //    path: settings.FileErrorPath,
                //    outputTemplate: settings.OutputTemplate,
                //    restrictedToMinimumLevel: LogEventLevel.Error,
                //    rollingInterval: RollingInterval.Day,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))

                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(settings.Uri))
                {
                    AutoRegisterTemplate = true,
                    MinimumLogEventLevel = logLevel,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    IndexFormat = settings.IndexFormat
                })
                .CreateLogger();
        }

        public void LogDebug(string messageTemplate, params object[] propertyValues)
        {
            _mainLogger.Debug(messageTemplate, propertyValues);
            _nLogMain.Debug($"[{_guid}] {messageTemplate}", propertyValues);
        }

        public void LogError(string messageTemplate, params object[] propertyValues)
        {
            _mainLogger.Error(messageTemplate, propertyValues);
            _nLogMain.Error($"[{_guid}] {messageTemplate}", propertyValues);

        }

        public void LogRequestWithResponse(string messageTemplate, params object[] propertyValues)
        {
            _mainLogger.Verbose(messageTemplate, propertyValues);
            _nLogMain.Trace($"[{_guid}] {messageTemplate}", propertyValues);
        }

        public void LogError(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _mainLogger.Error(exception, messageTemplate, propertyValues);
            _nLogMain.Error(exception, $"[{_guid}] {messageTemplate}", propertyValues);
        }
    }
}
