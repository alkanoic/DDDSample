using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Logging
{
    public class Log4netLogger : ILogger
    {

        private readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string DebugMemberFileLine(string memberName, string filePath, int lineNumber)
        {
            return $"{filePath}\t行:{lineNumber}\t{memberName}";
        }

        private const int LogQueueSize = 5;

        private readonly Queue<string> LogQueue = new Queue<string>(LogQueueSize);


        public void SqlLog(string sql, object param,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1
            )
        {
        }

        public void Write(LogLevel level, string log, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            var writeLog = $"{DateTime.Now}\t{DebugMemberFileLine(memberName, filePath, lineNumber)}\t{log}";
            LogQueue.Enqueue(writeLog);
            if (LogQueue.Count - 1 >= LogQueueSize) LogQueue.Dequeue();

            switch (level)
            {
                case LogLevel.Debug:
                    logger.Debug(writeLog);
                    break;
                case LogLevel.Info:
                    logger.Info(writeLog);
                    break;
                case LogLevel.Warn:
                    logger.Warn(writeLog);
                    break;
                case LogLevel.Error:
                    while (LogQueue.Count > 0)
                    {
                        logger.Error(LogQueue.Dequeue());
                    }
                    break;
                case LogLevel.Fatal:
                    while (LogQueue.Count > 0)
                    {
                        logger.Fatal(LogQueue.Dequeue());
                    }
                    break;
            }
        }

        public void Write(LogLevel level, Exception log, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            if (log is null) return;
            Write(level, log.ToString(), memberName, filePath, lineNumber);
        }

        public void Write(LogLevel level, string message, object param, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            var value = message;
            if (param != null)
            {
                value = $"{message}\t{GetPropertyValues(param)}";
            }
            Write(level, value, memberName, filePath, lineNumber);
        }

        public void WriteSql(LogLevel level, string sql, object param, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
            if(param != null)
            {
                Write(level, GetPropertyValues(param), memberName, filePath, lineNumber);
            }
            Write(LogLevel.Debug, sql, memberName, filePath, lineNumber);
        }

        private string GetPropertyValues(object param)
        {
            var builder = new StringBuilder();
            foreach (var pinfo in param.GetType().GetProperties())
            {
                var value = pinfo.GetValue(param);
                value = value is null ? "null" : value.ToString();
                builder.Append($"@{pinfo.Name}={value}\t");
            }
            return builder.ToString();
        }
    }
}
