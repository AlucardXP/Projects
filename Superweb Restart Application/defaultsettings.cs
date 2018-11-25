using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Superweb_Restart_Application
{
    public partial class defaultsettings : Form
    {
        public defaultsettings()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("You have not selected a Computer Setup", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (radioButton3.Checked == false && radioButton4.Checked == false)
            {
                MessageBox.Show("You have not selected an Ink Setup", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                string c = null;
                string i = null;
                string s = null;

                if (radioButton1.Checked == true)
                {
                    c = "Moby";
                }
                else if (radioButton2.Checked == true)
                {
                    c = "Gymea";
                }

                if (radioButton3.Checked == true)
                {
                    i = "QAMK";
                }
                else if (radioButton4.Checked == true)
                {
                    i = "RIT";
                }
                
                if (checkBox1.Checked == true)
                {
                    s = "Enabled";
                }
                else if (checkBox1.Checked == false)
                {
                    s = "Disabled";
                }

                DialogResult dialogResult1 = MessageBox.Show("Please confirm your settings are correct. \nComputers: " + c + "\nInk: " + i + "\nStitching" + s + "", "Confirm your settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult1 == DialogResult.Yes)
                {
                    SaveSettings();
                }
            }
        }

        private void SaveSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //Write changes to Config

            //Xitron Computers
            config.AppSettings.Settings["Rip 1"].Value = "192.168.130.21";
            config.AppSettings.Settings["Rip 2"].Value = "192.168.130.22";
            config.AppSettings.Settings["Rip 3"].Value = "192.168.130.23";
            config.AppSettings.Settings["Rip 4"].Value = "192.168.130.24";

            //Boreggo Computers
            config.AppSettings.Settings["Borrego 1"].Value = "192.168.130.11";
            config.AppSettings.Settings["Borrego 2"].Value = "192.168.130.12";
            config.AppSettings.Settings["Borrego 3"].Value = "192.168.130.13";
            config.AppSettings.Settings["Borrego 4"].Value = "192.168.130.14";

            //Beginning of the boring Moby or Gymea and QAMK and RIT check
            // Moby and QAMK
            if (radioButton1.Checked == true && radioButton3.Checked == true)
            {
                config.AppSettings.Settings["Gymea_Check"].Value = "0";
                config.AppSettings.Settings["qamk_check"].Value = "1";
                config.AppSettings.Settings["Moby 1"].Value = "192.168.130.111";
                config.AppSettings.Settings["Moby 2"].Value = "192.168.130.121";
                config.AppSettings.Settings["Moby 3"].Value = "192.168.130.131";
                config.AppSettings.Settings["Moby 4"].Value = "192.168.130.141";
            }

            // Moby and RIT
            if (radioButton1.Checked == true && radioButton4.Checked == true)
            {
                config.AppSettings.Settings["Gymea_Check"].Value = "0";
                config.AppSettings.Settings["qamk_check"].Value = "0";
                config.AppSettings.Settings["Moby 1 RIT"].Value = "192.168.131.41";
                config.AppSettings.Settings["Moby 2 RIT"].Value = "192.168.132.41";
                config.AppSettings.Settings["Moby 3 RIT"].Value = "192.168.133.41";
                config.AppSettings.Settings["Moby 4 RIT"].Value = "192.168.134.41";
                CheckBATrit();
            }

            // Gymea and QAMK
            if (radioButton2.Checked == true && radioButton3.Checked == true)
            {
                config.AppSettings.Settings["Gymea_Check"].Value = "1";
                config.AppSettings.Settings["qamk_check"].Value = "1";
                config.AppSettings.Settings["Gymea 1 QAMK"].Value = "192.168.130.11";
                config.AppSettings.Settings["Gymea 2 QAMK"].Value = "192.168.130.12";
                config.AppSettings.Settings["Gymea 3 QAMK"].Value = "192.168.130.13";
                config.AppSettings.Settings["Gymea 4 QAMK"].Value = "192.168.130.14";
                //checkBATgymea();
            }

            // Gymea and RIT
            if (radioButton2.Checked == true && radioButton4.Checked == true)
            {
                config.AppSettings.Settings["Gymea_Check"].Value = "1";
                config.AppSettings.Settings["qamk_check"].Value = "0";
                config.AppSettings.Settings["Gymea 1"].Value = "192.168.130.11";
                config.AppSettings.Settings["Gymea 2"].Value = "192.168.130.12";
                config.AppSettings.Settings["Gymea 3"].Value = "192.168.130.13";
                config.AppSettings.Settings["Gymea 4"].Value = "192.168.130.14";
                //checkBATgymea();
            }

            // Stitching
            if (checkBox1.Checked == true)
            {
                config.AppSettings.Settings["Stitching"].Value = "True";
            }
            else if (checkBox1.Checked == false)
            {
                config.AppSettings.Settings["Stitching"].Value = "False";
            }

            //First Time Setup
            config.AppSettings.Settings["First_Time"].Value = "1";

            //Computer Names
            GetA1Name("192.168.130.11");
            config.AppSettings.Settings["A1"].Value = label2.Text;
            GetA2Name("192.168.130.12");
            config.AppSettings.Settings["A2"].Value = label3.Text;
            GetA3Name("192.168.130.13");
            config.AppSettings.Settings["A3"].Value = label4.Text;
            GetA4Name("192.168.130.14");
            config.AppSettings.Settings["A4"].Value = label5.Text;

            //Save Config
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");




            DialogResult result = MessageBox.Show("Your Settings have been Saved", "Saved!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        // First time deployment to RIT and Gymea based systems
        // Check to see if batch file for each boreggo exists
        // If it exists then we will skip this else 
        // We will create each batch file for the Boreggos
        // REMINDER!!! Insert command to add and check when RIT or Gymea is selected

        // Check Moby RIT and Write BAT for RIT

        private void CheckBATrit()
        {
            //Write something here that will go into each boreggo and find BAT file

            string[] aspen = { "\\ASPEN1", "\\ASPEN2", "\\ASPEN3", "\\ASPEN4" };

            foreach (string ASPEN in aspen)
            {
                string MOBY = "";
                if (ASPEN == "\\ASPEN1")
                {
                    MOBY = "Moby 1 RIT";
                }
                if (ASPEN == "\\ASPEN2")
                {
                    MOBY = "Moby 2 RIT";
                }
                if (ASPEN == "\\ASPEN3")
                {
                    MOBY = "Moby 3 RIT";
                }
                if (ASPEN == "\\ASPEN4")
                {
                    MOBY = "Moby 4 RIT";
                }

                if (System.IO.File.Exists(@"\\" + ASPEN + @"\C$\putty\mobyrestart.bat") == true)
                {
                    MessageBox.Show("This file already exists on " + ASPEN + ". \n Skipping BAT creation");
                }
                else
                {
                    WriteBATrit(ASPEN, MOBY);

                }
            }
        }

        private void WriteBATrit(string ASPEN, string MOBY)
        {
            // Here we will create the batch files if they done exists

            try
            {
                CopyPLINK(ASPEN);
                CreateRITping(ASPEN, MOBY);
                string batFilePath = @"\\" + ASPEN + @"\C$\putty\mobyrestart.bat";

                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }

                using (StreamWriter sw = new StreamWriter(batFilePath))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("color 0a");
                    sw.WriteLine("title Moby RIT Restart Batch");
                    sw.WriteLine("echo Restarting Mobys's");
                    sw.WriteLine("plink.exe -ssh root@" + ConfigurationManager.AppSettings.Get(MOBY) + " -pw memjet1 reboot");
                    sw.WriteLine("echo Restart process is now complete");
                    // sw.WriteLine("pause");
                }
                CreateRITping(ASPEN, MOBY);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CreateRITping(string ASPEN, string MOBY)
        {
            try
            {
                string batFilePath = @"\\" + ASPEN + @"\C$\putty\pingmoby.bat";

                if (!File.Exists(batFilePath))
                {
                    using (FileStream fs = File.Create(batFilePath))
                    {
                        fs.Close();
                    }
                }

                using (StreamWriter sw = new StreamWriter(batFilePath))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("set \"host=" + ConfigurationManager.AppSettings.Get(MOBY) + "\"");
                    sw.WriteLine("");
                    sw.WriteLine("ping -n 1 \"%host%\" | findstr /r /c:\"[0-9] *ms\"");
                    sw.WriteLine("");
                    sw.WriteLine("if %errorlevel% == 0 (");
                    sw.WriteLine("    echo Success.");
                    sw.WriteLine("	exit /B 0");
                    sw.WriteLine(") else (");
                    sw.WriteLine("    echo FAILURE.");
                    sw.WriteLine("	exit /B 1");
                    sw.WriteLine(")");
                }
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CopyPLINK(string ASPEN)
        {
            string fileName = "plink.exe";
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string sourcePath = appPath + @"\deploy";
            string targetPath = @"\\" + ASPEN + @"\C$\putty";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory)
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                // Console.WriteLine("Source path does not exist!");
            }
        }
        private void GetA1Name(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception)
            {
                // Machine not found...
                MessageBox.Show("Maching NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label2.Text = machineName;
        }
        private void GetA2Name(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception)
            {
                // Machine not found...
                MessageBox.Show("Maching NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label3.Text = machineName;
        }
        private void GetA3Name(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception)
            {
                // Machine not found...
                MessageBox.Show("Maching NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label4.Text = machineName;
        }
        private void GetA4Name(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception)
            {
                // Machine not found...
                MessageBox.Show("Maching NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label5.Text = machineName;
        }
    }
}
