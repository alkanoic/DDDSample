using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample.DataModel.Tables;
using DDDSample.IRepository;

namespace DDDSample.Repository
{
    public class UserQueryRepository : RepositoryBase.RepositoryQueryBase, IRepository.ICommonQueryRepository<User, string>
    {

        public UserQueryRepository(Utility.Logging.ILogger logger)
        {
            this.logger = logger;
        }

        private const string SelectAllSql = @"
SELECT
    [UserId]
    ,[UserName]
    ,[UserStatus]
FROM [User]
";

        public IEnumerable<User> SelectAll()
        {
            return WithLogging((object e) =>
            {
                return DbConnection.Query<User>(SelectAllSql);
            }, SelectAllSql, null);
        }

        private const string SelectByIdSql = @"
SELECT
    [UserId]
    ,[UserName]
    ,[UserStatus]
FROM [User]
WHERE
    UserId = @UserId
";

        public User SelectById(string key)
        {
            return WithLogging((string e) =>
            {
                return DbConnection.Query<User>(SelectByIdSql, new { UserId = key }).SingleOrDefault();
            }, SelectAllSql, key);
        }

    }
}
