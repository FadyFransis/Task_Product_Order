using App.Common.Repositories;
using System;

namespace App.Common.Services.Logger
{
    public class LoggerService : Ilogger
    {
        LoggerRepository loggerRepository;
        public LoggerService()
        {
            loggerRepository = new LoggerRepository();
        }
        public LoggerService(Type type)
        {
            loggerRepository = new LoggerRepository(type);
        }
        public void Debug(object message, Exception ex = null)
        {
            loggerRepository.Debug(message, ex);
        }

        public void Error(object message, Exception ex = null)
        {
            loggerRepository.Error(message, ex);
        }

        public void Fatal(object message, Exception ex = null)
        {
            loggerRepository.Fatal(message, ex);
        }

        public void Info(object message, Exception ex = null)
        {
            loggerRepository.Info(message, ex);
        }

        public void Warn(object message, Exception ex = null)
        {
            loggerRepository.Warn(message, ex);
        }
    }

}
