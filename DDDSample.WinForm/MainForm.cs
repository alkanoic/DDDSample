using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDSample.DataModel.Tables;

namespace DDDSample.WinForms
{
    public partial class MainForm : Form
    {

        private readonly Utility.Logging.ILogger logger;

        private readonly Service.Command.IUserCommandService userCommandService;

        private readonly Service.Query.IUserQueryService userQueryService;

        public MainForm(Utility.Logging.ILogger logger, Service.Command.IUserCommandService userCommandService, Service.Query.IUserQueryService userQueryService)
        {
            this.logger = logger;
            this.userCommandService = userCommandService;
            this.userQueryService = userQueryService;

            InitializeComponent();
        }

        private void Btn_SubForm_Click(object sender, EventArgs e)
        {
            logger.Write(Utility.Logging.LogLevel.Info, "aaaaaa");
            using (var f = Program.container.GetInstance<SubForm>())
            {
                f.ShowDialog();
            }
        }

        private void Btn_UserInsert_Click(object sender, EventArgs e)
        {
            var u = new ServiceModel.InsertUserModel();
            u.UserId = Guid.NewGuid();
            u.UserName = DateTime.Now.ToString();
            u.UserStatus = 1;
            userCommandService.Insert(u);
        }

        private void Btn_ErrorLog_Click(object sender, EventArgs e)
        {
            logger.Write(Utility.Logging.LogLevel.Error, "errorlog");
        }

        private void Btn_UserSelect_Click(object sender, EventArgs e)
        {
            userQueryService.SelectAll();
        }

    }
}
