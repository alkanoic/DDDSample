using AutoMapper;
using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDSample.DataModel.Tables;

namespace DDDSample.WinForms
{
    static class Program
    {
        public static Container container;

        private static readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string connectionString = "ConnectionString";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Log4netInitialize();
            MapperInitialize();
            ContainerInitialize();
            Application.Run(container.GetInstance<MainForm>());
        }

        /// <summary>
        /// DIコンテナの初期化
        /// </summary>
        static void ContainerInitialize()
        {
            container = new Container();

            var sqlConnection = new ServiceBase.DbConnection.SqlServerServiceDbConnection();
            sqlConnection.ConnectionString = connectionString;
            container.Register<ServiceBase.DbConnection.IServiceDbConnection>(() => sqlConnection, Lifestyle.Transient);

            container.Register<Utility.Logging.ILogger, Utility.Logging.Log4netLogger>(Lifestyle.Singleton);
            container.Register<IRepository.ICommonCommandRepository<User, string>, Repository.UserCommandRepository>();
            container.Register<IRepository.ICommonQueryRepository<User, string>, Repository.UserQueryRepository>();
            container.Register<Service.Command.IUserCommandService, Service.Command.UserCommandService>();
            container.Register<Service.Query.IUserQueryService, Service.Query.UserQueryService>();

            container.Verify();
        }

        /// <summary>
        /// マッピング設定の初期化
        /// </summary>
        static void MapperInitialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ServiceModel.InsertUserModel, DataModel.Tables.User>();
            });
        }

        /// <summary>
        /// log4netの設定
        /// </summary>
        static void Log4netInitialize()
        {
            var adoappender = logger.Logger.Repository.GetAppenders().Single(x => x.Name == "AdoNetAppender") as log4net.Appender.AdoNetAppender;
            adoappender.ConnectionString = connectionString;
            adoappender.ActivateOptions();
        }

    }
}
