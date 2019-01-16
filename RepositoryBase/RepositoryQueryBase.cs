using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBase
{
    public abstract class RepositoryQueryBase : RepositoryBase
    {
        protected Utility.Logging.ILogger logger;

        protected TResult WithLogging<TArgs, TResult>(Func<TArgs, TResult> func, string sql, TArgs args,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            logger.WriteSql(Utility.Logging.LogLevel.Info, sql, args, memberName, filePath, lineNumber);
            var p = new Utility.Logging.PerformanceWatch();
            var result = func(args);
            var property = result.GetType().GetProperty("Count");
            if (property != null)
            {
                var count = property.GetValue(result);
                logger.Write(Utility.Logging.LogLevel.Info, $"RepositoryEnd\t{p.GetElapsedMilliseconds()}\t{result.ToString()}\t{count}", memberName, filePath, lineNumber);
            }
            else
            {
                logger.Write(Utility.Logging.LogLevel.Info, $"RepositoryEnd\t{p.GetElapsedMilliseconds()}\t{result.ToString()}", memberName, filePath, lineNumber);
            }
            return result;
        }


    }
}
