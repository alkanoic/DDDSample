using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.Repository.Test
{
    public static class CommonTestSetting
    {
        public static string UpdateUserId = "UpdateUserId";

        private static SQLiteConnection Connection;

        public static SQLiteConnection CreateSQLiteConnection()
        {
            DataModel.CommonDataModelSetting.CurrentUserId = CommonTestSetting.UpdateUserId;
            var connectionString = new SQLiteConnectionStringBuilder();
            connectionString.DataSource = ":memory:";
            Connection = new SQLiteConnection(connectionString.ConnectionString);
            Connection.Open();
            TableInitialize();
            return Connection;
        }

        private static void TableInitialize()
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = CreateUserTableSql;
                cmd.ExecuteNonQuery();
            }
        }

        private const string CreateUserTableSql = @"
CREATE TABLE [User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[UserName] NVARCHAR(30) NOT NULL,
	[UserStatus] INT NOT NULL, 
    [CreateOn] DATETIME NULL, 
    [UpdateOn] DATETIME NULL, 
    [UpdateUserId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
)
";
    }
}
