using ServiceBase.DbConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample.DataModel.Tables;
using DDDSample.IRepository;

namespace DDDSample.Service.Query
{
    public class UserQueryService : ServiceBase.QueryServiceBase, IUserQueryService
    {

        private readonly ICommonQueryRepository<User, string> userQueryRepository;

        public UserQueryService(ICommonQueryRepository<User, string> userQueryRepository, Utility.Logging.ILogger logger, IServiceDbConnection serviceDbConnection)
        {
            this.userQueryRepository = userQueryRepository;
            this.logger = logger;
            this.serviceDbConnection = serviceDbConnection;
        }

        protected override void RepositoryInitialize(IDbConnection connection)
        {
            userQueryRepository.DbConnection = connection;
        }

        public IEnumerable<User> SelectAll()
        {
            var result = WithLogging(() =>
            {
                return userQueryRepository.SelectAll();
            });
            return result;
        }

    }
}
