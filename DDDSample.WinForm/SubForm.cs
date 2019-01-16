using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDDSample.WinForms
{
    public partial class SubForm : Form
    {

        private readonly Utility.Logging.ILogger logger;

        public SubForm(Utility.Logging.ILogger logger)
        {
            this.logger = logger;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logger.Write(Utility.Logging.LogLevel.Info, "bbbb");
            this.Close();
        }
    }
}
