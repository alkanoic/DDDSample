using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase
{
    public abstract class CommandServiceBase : ServiceBase
    {

        private const string TransactionStartMessage = "TransactionStart";

        private const string TransactionCommitMessage = "TransactionCommit";


        public abstract void TransactionInitialize(IDbTransaction transaction);

        protected TResult WithTransaction<TResult>(Func<TResult> func)
        {
            using (var connection = serviceDbConnection.CreateDbConnection())
            {
                try
                {
                    connection.Open();
                    RepositoryInitialize(connection);
                    logger.Write(Utility.Logging.LogLevel.Info, TransactionStartMessage);
                    using (var tran = connection.BeginTransaction())
                    {
                        TransactionInitialize(tran);
                        var result = func();
                        tran.Commit();
                        logger.Write(Utility.Logging.LogLevel.Info, TransactionCommitMessage);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    logger.Write(Utility.Logging.LogLevel.Fatal, ex);
                    throw;
                }
            }
        }

        protected TResult WithLogTransaction<TResult>(Func<TResult> func, int retryCount = DefaultRetryCount, int sleepTime = DefaultRetrySleepTime,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            return WithLogging(() =>
            {
                return WithTransaction(() =>
                {
                    return func();
                });
            }, retryCount, sleepTime, memberName, filePath, lineNumber);
        }

        protected TResult WithLogTransaction<TArgs, TResult>(Func<TArgs, TResult> func, TArgs args, int retryCount = DefaultRetryCount, int sleepTime = DefaultRetrySleepTime,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = -1)
        {
            return WithLogging((TArgs args2) =>
            {
                return WithTransaction(() =>
                {
                    return func(args);
                });
            }, args, retryCount, sleepTime, memberName, filePath, lineNumber);
        }


    }
}
