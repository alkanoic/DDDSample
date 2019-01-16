using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample.DataModel.Tables;

namespace DDDSample.Repository
{
    public class UserCommandRepository : RepositoryBase.RepositoryCommandBase, IRepository.ICommonCommandRepository<User, string>
    {

        public IDbTransaction DbTransaction { get; set; }

        public UserCommandRepository(Utility.Logging.ILogger logger)
        {
            this.logger = logger;
        }

        private const string DeleteSql = @"
DELETE FROM [User]
      WHERE UserId = @UserId
";

        public int Delete(string key)
        {
            return WithLogging((string e) =>
            {
                return DbConnection.Execute(DeleteSql, key, DbTransaction);
            }, DeleteSql, key);
        }

        private const string InsertSql = @"
INSERT INTO [User]
(
    [UserId]
    ,[UserName]
    ,[UserStatus]
" + CommonColumnInsertSql + @"
)
VALUES
(
    @UserId
    ,@UserName
    ,@UserStatus
" + CommonColumnInsertSqlParam + @"
)
";

        public int Insert(User entity)
        {
            return WithLogging((User e) =>
            {
                return DbConnection.Execute(InsertSql, entity, DbTransaction);
            }, InsertSql, entity);
        }

        private const string UpdateSql = @"
UPDATE [User]
   SET [UserName] = @UserName
      ,[UserStatus] = @UserStatus
" + CommonColumnUpdateSql + @"
WHERE
    UserId = @UserId
";

        public int Update(User entity)
        {
            return WithLogging((User e) =>
            {
                return DbConnection.Execute(UpdateSql, entity, DbTransaction);
            }, UpdateSql, entity);
        }

    }
}
