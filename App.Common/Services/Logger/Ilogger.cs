using System;

namespace App.Common.Services.Logger
{
    public interface Ilogger
    {
        void Debug(object message, Exception ex = null);
        void Info(object message, Exception ex = null);
        void Warn(object message, Exception ex = null);
        void Error(object message, Exception ex = null);
        void Fatal(object message, Exception ex = null);
    }
}
