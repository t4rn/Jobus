using System;

namespace Jobus.Core.Services.Loggers
{
    public interface ILogger
    {
        void LogDebug(string messageTemplate, params object[] propertyValues);
        void LogError(string messageTemplate, params object[] propertyValues);
        void LogError(Exception exception, string messageTemplate, params object[] propertyValues);
        void LogRequestWithResponse(string messageTemplate, params object[] propertyValues);
    }
}
