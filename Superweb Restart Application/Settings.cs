using System;
using System.Windows.Forms;
using System.Configuration;
using System.Management;
using System.Linq;

namespace Superweb_Restart_Application
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            loadConfig();
            textBox13.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirmDialog = MessageBox.Show("Are you sure you want to Save?\n This will overwrite and saved settings.", "Please Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (confirmDialog == DialogResult.OK)
                {
                    saveConfig();
                    //Confirm Save
                    DialogResult result = MessageBox.Show("Your Settings have been Saved", "Saved!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (confirmDialog == DialogResult.No)
                {
                    // Dont do anything
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void saveConfig()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //Write changes to Config

            //Xitron Computers
            config.AppSettings.Settings["Rip 1"].Value = textBox1.Text;
            config.AppSettings.Settings["Rip 2"].Value = textBox2.Text;
            config.AppSettings.Settings["Rip 3"].Value = textBox3.Text;
            config.AppSettings.Settings["Rip 4"].Value = textBox4.Text;

            //Boreggo Computers
            config.AppSettings.Settings["Borrego 1"].Value = textBox5.Text;
            config.AppSettings.Settings["Borrego 2"].Value = textBox6.Text;
            config.AppSettings.Settings["Borrego 3"].Value = textBox7.Text;
            config.AppSettings.Settings["Borrego 4"].Value = textBox8.Text;

            //Mobys
            if (checkBox1.Checked == true)
            {
                config.AppSettings.Settings["qamk_check"].Value = "1";
                config.AppSettings.Settings["Gymea_Check"].Value = "0";
                config.AppSettings.Settings["Moby 1"].Value = textBox9.Text;
                config.AppSettings.Settings["Moby 2"].Value = textBox10.Text;
                config.AppSettings.Settings["Moby 3"].Value = textBox11.Text;
                config.AppSettings.Settings["Moby 4"].Value = textBox12.Text;
            }
            else if (checkBox2.Checked == true)
            {
                config.AppSettings.Settings["qamk_check"].Value = "0";
                config.AppSettings.Settings["Gymea_Check"].Value = "1";
                config.AppSettings.Settings["Gymea 1"].Value = textBox9.Text;
                config.AppSettings.Settings["Gymea 2"].Value = textBox10.Text;
                config.AppSettings.Settings["Gymea 3"].Value = textBox11.Text;
                config.AppSettings.Settings["Gymea 4"].Value = textBox12.Text;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                config.AppSettings.Settings["qamk_check"].Value = "1";
                config.AppSettings.Settings["Gymea_Check"].Value = "1";
                config.AppSettings.Settings["Gymea 1 QAMK"].Value = textBox9.Text;
                config.AppSettings.Settings["Gymea 2 QAMK"].Value = textBox10.Text;
                config.AppSettings.Settings["Gymea 3 QAMK"].Value = textBox11.Text;
                config.AppSettings.Settings["Gymea 4 QAMK"].Value = textBox12.Text;
            }
            else
            {
                config.AppSettings.Settings["qamk_check"].Value = "0";
                config.AppSettings.Settings["Gymea_Check"].Value = "0";
                config.AppSettings.Settings["Moby 1 RIT"].Value = textBox9.Text;
                config.AppSettings.Settings["Moby 2 RIT"].Value = textBox10.Text;
                config.AppSettings.Settings["Moby 3 RIT"].Value = textBox11.Text;
                config.AppSettings.Settings["Moby 4 RIT"].Value = textBox12.Text;
            }

            if (checkBox3.Checked == true)
            {
                config.AppSettings.Settings["Stitching"].Value = "True";
            }
            else if (checkBox3.Checked == false)
            {
                config.AppSettings.Settings["Stitching"].Value = "False";
            }

            //Save Config
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void loadConfig()
        {
            //Get Check value from XML Config
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string checkvalue = ConfigurationManager.AppSettings["qamk_check"];
            string checkvalue1 = ConfigurationManager.AppSettings["Gymea_Check"];
            if (checkvalue == "1")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (checkvalue1 == "1")
            {

            }
            else
            {
                checkBox2.Checked = false;
            }

            //Load Rip IP Addresses
            textBox1.Text = ConfigurationManager.AppSettings.Get("Rip 1");
            textBox2.Text = ConfigurationManager.AppSettings.Get("Rip 2");
            textBox3.Text = ConfigurationManager.AppSettings.Get("Rip 3");
            textBox4.Text = ConfigurationManager.AppSettings.Get("Rip 4");

            //Load Boreggo IP Addresses
            textBox5.Text = ConfigurationManager.AppSettings.Get("Borrego 1");
            textBox6.Text = ConfigurationManager.AppSettings.Get("Borrego 2");
            textBox7.Text = ConfigurationManager.AppSettings.Get("Borrego 3");
            textBox8.Text = ConfigurationManager.AppSettings.Get("Borrego 4");


            //Load Moby IP Addresses
            // Moby and QAMK
            if (checkBox1.Checked == true && checkBox2.Checked == false)
            {
                textBox9.Text = ConfigurationManager.AppSettings.Get("Moby 1");
                textBox10.Text = ConfigurationManager.AppSettings.Get("Moby 2");
                textBox11.Text = ConfigurationManager.AppSettings.Get("Moby 3");
                textBox12.Text = ConfigurationManager.AppSettings.Get("Moby 4");
            }
            // Gymea and RIT
            else if (checkBox2.Checked == true && checkBox1.Checked == false)
            {
                groupBox3.Text = "Gymea IP Addresses";
                label12.Text = "Gymea 1";
                label10.Text = "Gymea 2";
                label11.Text = "Gymea 3";
                label9.Text = "Gymea 4";
                textBox9.Text = ConfigurationManager.AppSettings.Get("Gymea 1");
                textBox10.Text = ConfigurationManager.AppSettings.Get("Gymea 2");
                textBox11.Text = ConfigurationManager.AppSettings.Get("Gymea 3");
                textBox12.Text = ConfigurationManager.AppSettings.Get("Gymea 4");
            }
            // Gymea and QAMK
            else if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                groupBox3.Text = "Gymea IP Addresses QAMK";
                label12.Text = "Gymea 1";
                label10.Text = "Gymea 2";
                label11.Text = "Gymea 3";
                label9.Text = "Gymea 4";
                textBox9.Text = ConfigurationManager.AppSettings.Get("Gymea 1 QAMK");
                textBox10.Text = ConfigurationManager.AppSettings.Get("Gymea 2 QAMK");
                textBox11.Text = ConfigurationManager.AppSettings.Get("Gymea 3 QAMK");
                textBox12.Text = ConfigurationManager.AppSettings.Get("Gymea 4 QAMK");
            }
            // Moby and RIT
            else if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                textBox9.Text = ConfigurationManager.AppSettings.Get("Moby 1 RIT");
                textBox10.Text = ConfigurationManager.AppSettings.Get("Moby 2 RIT");
                textBox11.Text = ConfigurationManager.AppSettings.Get("Moby 3 RIT");
                textBox12.Text = ConfigurationManager.AppSettings.Get("Moby 4 RIT");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                groupBox3.Text = "Gymea IP Addresses";
                label12.Text = "Gymea 1";
                label10.Text = "Gymea 2";
                label11.Text = "Gymea 3";
                label9.Text = "Gymea 4";
            }

            if (checkBox2.Checked == false)
            {
                groupBox3.Text = "Moby IP Addresses";
                label12.Text = "Moby 1";
                label10.Text = "Moby 2";
                label11.Text = "Moby 3";
                label9.Text = "Moby 4";
            }
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                groupBox3.Text = "Gymea QAMK IP Addresses";
                label12.Text = "Gymea 1";
                label10.Text = "Gymea 2";
                label11.Text = "Gymea 3";
                label9.Text = "Gymea 4";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox3.Text = "Moby QAMK IP Addresses";
                label12.Text = "Moby 1";
                label10.Text = "Moby 2";
                label11.Text = "Moby 3";
                label9.Text = "Moby 4";
            }

            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                groupBox3.Text = "Gymea QAMK IP Addresses";
                label12.Text = "Gymea 1";
                label10.Text = "Gymea 2";
                label11.Text = "Gymea 3";
                label9.Text = "Gymea 4";
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                groupBox3.Text = "Moby RIT IP Addresses";
                label12.Text = "Moby 1";
                label10.Text = "Moby 2";
                label11.Text = "Moby 3";
                label9.Text = "Moby 4";
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Help frm = new Help();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Password frm = new Password();
            
            DialogResult dialogresult = frm.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                passwordOK();
            }

        }

        public void passwordOK()
        {
            // Xitron Textboxes
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;

            // Borrego Textboxes
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;

            //Moby and Gymea
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;

            //Buttons
            button2.Visible = true;
            button3.Visible = true;
            button5.Visible = true;

            //Checkboxes
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            defaultsettings frm = new defaultsettings();
            frm.ShowDialog();
        }

    }
}