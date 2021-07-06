using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace App.Common.Repositories
{
    internal class LoggerRepository
    {
        private readonly log4net.ILog _log;

        public LoggerRepository()
        {
            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            var logRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public LoggerRepository(Type type)
        {
            _log = log4net.LogManager.GetLogger(type);
        }

        public void Debug(object message, Exception ex = null)
        {
            if (_log.IsDebugEnabled)
            {
                if (ex == null)
                {
                    _log.Debug(message);
                }
                else
                {
                    _log.Debug(message, ex);
                }
            }
        }

        public void Info(object message, Exception ex = null)
        {
            if (_log.IsInfoEnabled)
            {
                if (ex == null)
                {
                    _log.Info(message);
                }
                else
                {
                    _log.Info(message, ex);
                }
            }
        }

        public void Warn(object message, Exception ex = null)
        {
            if (_log.IsWarnEnabled)
            {
                if (ex == null)
                {
                    _log.Warn(message);
                }
                else
                {
                    _log.Warn(message, ex);
                }
            }
        }

        public void Error(object message, Exception ex = null)
        {
            if (_log.IsErrorEnabled)
            {
                if (ex == null)
                {
                    _log.Error(message);
                }
                else
                {
                    _log.Error(message, ex);
                }
            }
        }

        public void Fatal(object message, Exception ex = null)
        {
            if (_log.IsFatalEnabled)
            {
                if (ex == null)
                {
                    _log.Fatal(message);
                }
                else
                {
                    _log.Fatal(message, ex);
                }
            }
        }
    }

}
