using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superweb_Restart_Application
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        public void PasswordText(string s)
        {
            textBox1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "97lamar")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
              DialogResult result = MessageBox.Show("You did enter the correct password!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Dennis frm = new Dennis();
                frm.ShowDialog();
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }
    }
}
