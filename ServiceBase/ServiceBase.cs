using ServiceBase.DbConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase
{
    public abstract class ServiceBase
    {

        protected Utility.Logging.ILogger logger;

        protected IServiceDbConnection serviceDbConnection;

        protected const string ServiceStartMessage = "ServiceStart";

        protected const string ServiceEndMessage = "ServiceEnd";

        protected const string ServiceExceptionMessage = "Repository層でエラーが発生しました。";

        protected const int DefaultRetryCount = 3;

        protected const int DefaultRetrySleepTime = 10;

        protected abstract void RepositoryInitialize(IDbConnection connection);

        protected TResult WithLogging<TResult>(Func<TResult> func, int retryCount = DefaultRetryCount, int sleepTime = DefaultRetrySleepTime,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            do
            {
                try
                {
                    using(var connection = serviceDbConnection.CreateDbConnection())
                    {
                        RepositoryInitialize(connection);
                        logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceStartMessage}\t{retryCount}", memberName, filePath, lineNumber);
                        var p = new Utility.Logging.PerformanceWatch();
                        var result = func();
                        logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceEndMessage}\t{p.GetElapsedMilliseconds()}\t{result}", memberName, filePath, lineNumber);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    logger.Write(Utility.Logging.LogLevel.Error, ex, memberName, filePath, lineNumber);
                    retryCount -= 1;
                    if (retryCount == 0)
                    {
                        throw;
                    }
                }
            } while (retryCount != 0);
            return default(TResult);
        }

        protected TResult WithLogging<TArgs, TResult>(Func<TArgs, TResult> func, TArgs args, int retryCount = DefaultRetryCount, int sleepTime = DefaultRetrySleepTime,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            do
            {
                try
                {
                    using (var connection = serviceDbConnection.CreateDbConnection())
                    {
                        RepositoryInitialize(connection);
                        logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceStartMessage}\t{retryCount}\t{args.ToString()}", memberName, filePath, lineNumber);
                        var p = new Utility.Logging.PerformanceWatch();
                        var result = func(args);
                        logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceEndMessage}\t{p.GetElapsedMilliseconds()}\t{result}", memberName, filePath, lineNumber);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    logger.Write(Utility.Logging.LogLevel.Error, ex, memberName, filePath, lineNumber);
                    retryCount -= 1;
                    if (retryCount == 0)
                    {
                        throw;
                    }
                }
            } while (retryCount != 0);
            return default(TResult);
        }


    }
}
