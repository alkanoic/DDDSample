using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DDDSample.Repository.Test
{
    public class UserRepositoryTest
    {

        private readonly IDbConnection connection;

        private readonly UserCommandRepository commandRepository;
        private readonly UserQueryRepository queryRepository;

        public UserRepositoryTest()
        {
            connection = CommonTestSetting.CreateSQLiteConnection();
            commandRepository = new UserCommandRepository(new Utility.Logging.MockLogger());
            queryRepository = new UserQueryRepository(new Utility.Logging.MockLogger());
            commandRepository.DbConnection = connection;
            queryRepository.DbConnection = connection;
        }

        [Fact]
        public void InsertSelect()
        {
            // Arrange
            var user = new DataModel.Tables.User();
            user.UserId = Guid.NewGuid();
            user.UserName = "TestUser";
            user.UserStatus = 3;

            // Act
            int result;
            using(var tran = connection.BeginTransaction())
            {
                commandRepository.DbTransaction = tran;
                result = commandRepository.Insert(user);
                tran.Commit();
            }

            // Assert
            Assert.Equal(1, result);
            var results = queryRepository.SelectAll();
            Assert.Single(results);
            var single = results.Single();
            Assert.Equal(user.UserId, single.UserId);
            Assert.Equal(user.UserName, single.UserName);
            Assert.Equal(user.UserStatus, single.UserStatus);
            Assert.NotEqual(new DateTime(), user.CreateOn);
            Assert.NotEqual(new DateTime(), user.UpdateOn);
            Assert.Equal(CommonTestSetting.UpdateUserId, user.UpdateUserId);
        }

    }
}
