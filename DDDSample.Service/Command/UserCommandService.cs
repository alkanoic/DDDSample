using AutoMapper;
using log4net.Core;
using ServiceBase;
using ServiceBase.DbConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DDDSample.DataModel.Tables;
using DDDSample.IRepository;
using DDDSample.ServiceModel;

namespace DDDSample.Service.Command
{
    public class UserCommandService : ServiceBase.CommandServiceBase, IUserCommandService
    {

        private readonly ICommonCommandRepository<User, string> userCommandRepository;

        public UserCommandService(ICommonCommandRepository<User, string> userCommandRepository, Utility.Logging.ILogger logger, IServiceDbConnection serviceDbConnection)
        {
            this.userCommandRepository = userCommandRepository;
            this.logger = logger;
            this.serviceDbConnection = serviceDbConnection;
        }

        protected override void RepositoryInitialize(IDbConnection connection)
        {
            userCommandRepository.DbConnection = connection;
        }

        public override void TransactionInitialize(IDbTransaction transaction)
        {
            userCommandRepository.DbTransaction = transaction;
        }

        public void Insert(InsertUserModel u)
        {
            var ud = Mapper.Map<User>(u);
            var result = WithLogTransaction((User user) =>
            {
                return userCommandRepository.Insert(user);
            }, ud);
        }

        public void Insert2(InsertUserModel u)
        {
            var ud = Mapper.Map<User>(u);
            Func<User, IDbTransaction, ServiceResult<int>> f = (User user, IDbTransaction tran) =>
            {
                var a = userCommandRepository.Insert(user);
                var result = new ServiceResult<int>();
                result.Success = true;
                result.Result = a;
                return result;
            };
        }

    }
}
