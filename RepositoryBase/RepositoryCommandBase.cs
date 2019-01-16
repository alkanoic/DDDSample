using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBase
{
    public abstract class RepositoryCommandBase : RepositoryBase
    {
        protected Utility.Logging.ILogger logger;

        protected const string CommonColumnInsertSql = ",UpdateUserId, CreateOn, UpdateOn";

        protected const string CommonColumnInsertSqlParam = ",@UpdateUserId, @CreateOn, @UpdateOn";

        protected const string CommonColumnUpdateSql = ",UpdateUserId = @UpdateUserId, UpdateOn = @UpdateOn";

        protected TResult WithLogging<TArgs, TResult>(Func<TArgs, TResult> func, string sql, TArgs args,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            logger.WriteSql(Utility.Logging.LogLevel.Info, sql, args, memberName, filePath, lineNumber);
            var p = new Utility.Logging.PerformanceWatch();
            var result = func(args);
            logger.Write(Utility.Logging.LogLevel.Info, $"RepositoryEnd\t{p.GetElapsedMilliseconds()}\t{result.ToString()}", memberName, filePath, lineNumber);
            return result;
        }

    }
}
