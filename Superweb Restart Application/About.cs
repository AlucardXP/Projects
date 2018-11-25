using System;
using System.Configuration;
using System.Windows.Forms;

namespace Superweb_Restart_Application
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            label2.Text = ConfigurationManager.AppSettings["Version"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
