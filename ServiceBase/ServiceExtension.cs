using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase
{
    public static class ServiceExtension
    {

        private const string ServiceStartMessage = "ServiceStart";

        private const string ServiceEndMessage = "ServiceEnd";

        private const string TransactionStartMessage = "TransactionStart";

        private const string TransactionCommitMessage = "TransactionCommit";

        private const string ServiceExceptionMessage = "データアクセス時にエラーが発生しました。";

        private const int DefaultRetryCount = 3;

        private const int DefaultRetrySleepTime = 10;


        public static Func<ServiceResult<TResult>> ServiceWithLogging<TResult>(this Func<ServiceResult<TResult>> f, Utility.Logging.ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            return () =>
            {
                logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceStartMessage}", memberName, filePath, lineNumber);
                var p = new Utility.Logging.PerformanceWatch();
                var result = f();
                logger.Write(Utility.Logging.LogLevel.Info, $"{ServiceEndMessage}\t{p.GetElapsedMilliseconds()}", memberName, filePath, lineNumber);
                return result;
            };
        }

        public static Func<TArgs, IDbTransaction, ServiceResult<TResult>> ServiceWithTransaction<TArgs, TResult>(this Func<TArgs, IDbTransaction,
            ServiceResult<TResult>> f,
            Utility.Logging.ILogger logger,
            IDbConnection connection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            return (TArgs args, IDbTransaction t) =>
            {
                try
                {
                    connection.Open();
                    logger.Write(Utility.Logging.LogLevel.Info, TransactionStartMessage);
                    using (var tran = connection.BeginTransaction())
                    {
                        var result = f(args, tran);
                        tran.Commit();
                        logger.Write(Utility.Logging.LogLevel.Info, TransactionCommitMessage);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    logger.Write(Utility.Logging.LogLevel.Fatal, ex);
                    throw new ServiceException(ServiceExceptionMessage, ex);
                }
                finally
                {
                    connection.Close();
                }
            };
        }

    }
}
