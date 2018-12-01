using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Configuration;
using Renci.SshNet;
using Microsoft.Web.Administration;
using System.Linq;
using AutoUpdaterDotNET;
using System.Management;
using System.Drawing;
using System.Net;

namespace Superweb_Restart_Application
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            toolStripStatusLabel1.Text = "";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Point to where the auto updater XML file is located
            AutoUpdater.Start("http://www.xpshosting.com/sw/update/update.xml");

            string checkvalue = ConfigurationManager.AppSettings["qamk_check"];
            string checkvalue1 = ConfigurationManager.AppSettings["Gymea_Check"];
            string checkstitching = ConfigurationManager.AppSettings["Stitching"];

            // First Time Startup Check
            string firsttime = ConfigurationManager.AppSettings["First_Time"];
            if (firsttime == "0")
            {
                DialogResult result = MessageBox.Show("Please go through the First Time Setup Menu", "First Time Setup",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                defaultsettings frm = new defaultsettings();
                frm.ShowDialog();
            }

            // Initialize Drop Box
            comboBox1.Items.Add("Aspen Services");
            comboBox1.Items.Add("Borrego Computers");
            if (checkvalue == "1")
            {
                comboBox1.Items.Add("Moby Computers");
            }
            else if (checkvalue == "0")
            {
                comboBox1.Items.Add("Moby Computers - RIT");
            }
            else if (checkvalue1 == "1")
            {
                comboBox1.Items.Add("Gymea Computers");
            }
            comboBox1.Items.Add("Xitron Rip Computers");

            // Stitching
            //if (checkstitching == "True")
            //{
            //    label2.Visible = true;
            //    StitchingStripMenuItem1.Visible = true;
            //    stitchingStatus();
            //}
            //else if (checkstitching == "False")
            //{
            //    label2.Visible = false;
            //    StitchingStripMenuItem1.Visible = false;
            //}
            //Timer timer = new Timer();
            //timer.Interval = (10 * 1000); // 10 secs
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings frm = new Settings();
            frm.ShowDialog();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status frm = new Status();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1();
            frm.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                DialogResult result = MessageBox.Show("You did not select anything to restart!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (checkedListBox1.SelectedIndex == -1)
            {
                DialogResult result = MessageBox.Show("You did not select anything to restart!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Gather all checked items
                if (checkedListBox1.CheckedItems.Count != 0)
                {
                    int i;
                    string s;
                    s = "Are you sure you want to restart the following:\n";
                    for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                    {
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            s = s + checkedListBox1.Items[i].ToString() + "\n";
                        }
                    }
                    DialogResult result = MessageBox.Show(s, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                    {
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            if (result == DialogResult.Yes)
                            {
                                //Code for Yes

                                ProgressDialog progressDialog = new ProgressDialog();
                                for (int n = 0; n < 100; n++)
                                {
                                    progressDialog.UpdateProgress(n); // Update progress in progressDialog
                                }


                                toolStripStatusLabel2.Text = "Restarting:";
                                toolStripStatusLabel1.Text = ConfigurationManager.AppSettings.Get(checkedListBox1.Items[i].ToString());
                                toolStripStatusLabel3.Text = checkedListBox1.Items[i].ToString();
                                progressDialog.ChangeLabel("Restarting: " + toolStripStatusLabel3.Text);
                                progressDialog.Show();
                                reboot();

                                toolStripStatusLabel2.Text = "Status:";
                                toolStripStatusLabel3.Text = "Idle";

                                progressDialog.Close();
                            }
                            else if (result == DialogResult.No)
                            {
                                //code for No
                            }
                        }
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Called when a new index is selected.
            if (comboBox1.Text == "Aspen Services")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Aspen 1");
                checkedListBox1.Items.Add("Aspen 2");
                checkedListBox1.Items.Add("Aspen 3");
                checkedListBox1.Items.Add("Aspen 4");
                checkedListBox1.Items.Add("Cedar");
                checkedListBox1.Items.Add("SuperCloud");
            }
            else if (comboBox1.Text == "Borrego Computers")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Borrego 1");
                checkedListBox1.Items.Add("Borrego 2");
                checkedListBox1.Items.Add("Borrego 3");
                checkedListBox1.Items.Add("Borrego 4");
            }
            else if (comboBox1.Text == "Moby Computers")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Moby 1");
                checkedListBox1.Items.Add("Moby 2");
                checkedListBox1.Items.Add("Moby 3");
                checkedListBox1.Items.Add("Moby 4");
            }
            else if (comboBox1.Text == "Moby Computers - RIT")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Moby 1 - RIT");
                checkedListBox1.Items.Add("Moby 2 - RIT");
                checkedListBox1.Items.Add("Moby 3 - RIT");
                checkedListBox1.Items.Add("Moby 4 - RIT");
            }
            else if (comboBox1.Text == "Xitron Rip Computers")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Rip 1");
                checkedListBox1.Items.Add("Rip 2");
                checkedListBox1.Items.Add("Rip 3");
                checkedListBox1.Items.Add("Rip 4");
            }
            else if (comboBox1.Text == "Gymea Computers")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Gymea 1");
                checkedListBox1.Items.Add("Gymea 2");
                checkedListBox1.Items.Add("Gymea 3");
                checkedListBox1.Items.Add("Gymea 4");
            }
            else if (comboBox1.Text == "Gymea Computers - QAMK")
            {
                checkedListBox1.Items.Clear();
                checkedListBox1.Visible = true;
                checkedListBox1.Items.Add("Gymea 1");
                checkedListBox1.Items.Add("Gymea 2");
                checkedListBox1.Items.Add("Gymea 3");
                checkedListBox1.Items.Add("Gymea 4");
            }
        }
        // Reboot Function
        private void reboot()
        {
            // Use cmd shutdown if Boreggo is chosen
            if (comboBox1.Text == "Borrego Computers")
            {
                try
                {
                    var rebootWindows = new Process();
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c\"" + string.Format("shutdown /m \\\\{0} /f /t 00 /r", toolStripStatusLabel1.Text) + "\""
                    };
                    rebootWindows.StartInfo = startInfo;
                    rebootWindows.Start();
                    rebootWindows.WaitForExit();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // Use cmd shutdown if Rip is chosen
            else if (comboBox1.Text == "Xitron Rip Computers")
            {
                try
                {
                    var rebootWindows = new Process();
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c\"" + string.Format("shutdown /m \\\\{0} /f /t 00 /r", toolStripStatusLabel1.Text) + "\""
                    };
                    rebootWindows.StartInfo = startInfo;
                    rebootWindows.Start();
                    rebootWindows.WaitForExit();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // Use iisreset for Aspen Services
            else if (comboBox1.Text == "Aspen Services")
            {
                iisRestart();
            }
            // Using SSH.NET package. "host","port","user","password"
            else if (comboBox1.Text == "Moby Computers")
            {
                using (SshClient ssh = new SshClient(toolStripStatusLabel1.Text, "root", "memjet1"))
                {
                    ssh.Connect();
                    var result = ssh.RunCommand("reboot");
                    ssh.Disconnect();
                }

            }
            else if (comboBox1.Text == "Moby Computers - RIT")
            {
                mobyRITReboot();
            }
        }

        private void mobyRITReboot()
        {
            // Remote to each Boreggo and start a restart command for 4 Gymeas per machine
            // Use a batch file on the remote Boreggo that will launch plink and reboot all gymeas associated with each boreggo
            string aspen = "";
            string filePath = @"c:\putty\psexec.exe";

            if (toolStripStatusLabel1.Text == "192.168.131.41")
            {
                aspen = ConfigurationManager.AppSettings.Get("Borrego 1");
            }
            if (toolStripStatusLabel1.Text == "192.168.132.41")
            {
                aspen = ConfigurationManager.AppSettings.Get("Borrego 2");
            }
            if (toolStripStatusLabel1.Text == "192.168.133.41")
            {
                aspen = ConfigurationManager.AppSettings.Get("Borrego 3");
            }
            if (toolStripStatusLabel1.Text == "192.168.134.41")
            {
                aspen = ConfigurationManager.AppSettings.Get("Borrego 4");
            }

            try
            {
                Process processInfo = new Process();
                processInfo.StartInfo.FileName = filePath;
                processInfo.StartInfo.Arguments = @"\\" + aspen + @" C:\putty\restartmoby.bat";
                processInfo.StartInfo.UseShellExecute = false;
                processInfo.StartInfo.CreateNoWindow = true;
                processInfo.StartInfo.RedirectStandardError = true;
                processInfo.StartInfo.RedirectStandardOutput = true;
                processInfo.StartInfo.RedirectStandardInput = true;
                processInfo.Start();

                processInfo.WaitForExit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void iisRestart()
        {
            var serverName = "";
            if (toolStripStatusLabel1.Text == "Aspen 1")
            {
                serverName = ConfigurationManager.AppSettings.Get("A1");
            }
            if (toolStripStatusLabel1.Text == "Aspen 2")
            {
                serverName = ConfigurationManager.AppSettings.Get("A2");
            }
            if (toolStripStatusLabel1.Text == "Aspen 3")
            {
                serverName = ConfigurationManager.AppSettings.Get("A3");
            }
            if (toolStripStatusLabel1.Text == "Aspen 4")
            {
                serverName = ConfigurationManager.AppSettings.Get("A4");
            }

            var appPoolName = "";
            if (toolStripStatusLabel3.Text == "Cedar")
            {
                serverName = ConfigurationManager.AppSettings.Get("A4");
                appPoolName = ".NET 4.Cedar";
            }
            else if (toolStripStatusLabel3.Text == "SuperCloud")
            {
                serverName = ConfigurationManager.AppSettings.Get("A4");
                appPoolName = ".NET 4.OEM";
            }
            else
            {
                appPoolName = ".NET 4.Aspen";
            }

            if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(appPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(serverName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == appPoolName);

                        //Don't bother trying to recycle if we don't have an app pool
                        if (appPool != null)
                        {
                            //Get the current state of the app pool
                            bool appPoolRunning = appPool.State == ObjectState.Started || appPool.State == ObjectState.Starting;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;

                            //The app pool is running, so stop it first.
                            if (appPoolRunning)
                            {
                                //Wait for the app to finish before trying to stop
                                while (appPool.State == ObjectState.Starting) { System.Threading.Thread.Sleep(1000); }

                                //Stop the app if it isn't already stopped
                                if (appPool.State != ObjectState.Stopped)
                                {
                                    appPool.Stop();
                                }
                                appPoolStopped = true;
                            }

                            //Only try restart the app pool if it was running in the first place, because there may be a reason it was not started.
                            if (appPoolStopped && appPoolRunning)
                            {
                                //Wait for the app to finish before trying to start
                                while (appPool.State == ObjectState.Stopping) { System.Threading.Thread.Sleep(1000); }

                                //Start the app
                                appPool.Start();
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format("An Application Pool does not exist with the name {0}.{1}", serverName, appPoolName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Unable to restart the application pools for {0}.{1}", serverName, appPoolName), ex.InnerException);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://www.xpshosting.com/sw/update/update.xml");
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dialogResult =
                        MessageBox.Show(
                            string.Format(
                                "There is new version {0} avilable. You are using version {1}. Do you want to update the application now?",
                                args.CurrentVersion, args.InstalledVersion), @"Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            //You can use Download Update dialog used by AutoUpdater.NET to download the update.

                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"There is no update avilable please try again later.", @"No update available",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                       @"There is a problem reaching update server please check your internet connection and try again later.",
                       @"Update check failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultsettings frm = new defaultsettings();
            frm.ShowDialog();
        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Test frm = new Test();
            frm.ShowDialog();
        }

        private void stitchingStatus()
        {
            //string machineName = string.Empty;
            //IPHostEntry hostEntry = Dns.GetHostEntry("192.168.130.14");
            //machineName = hostEntry.HostName;

            string ServerName = ConfigurationManager.AppSettings.Get("A4");
            string regKeyToGet = @"SOFTWARE\Wow6432Node\Memjet\Aspen\Controller";
            string keyToRead = "StitchOverlapMicrons";

            ConnectionOptions oConn = new ConnectionOptions();
            //oConn.Username = "Superweb";
            //oConn.Password = "memjet1";
            System.Management.ManagementScope scope = new System.Management.ManagementScope(@"\\" + ServerName + @"\root\default", oConn);

            scope.Options.EnablePrivileges = true;
            scope.Connect();

            ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
            ManagementBaseObject inParams = registry.GetMethodParameters("GetStringValue");

            inParams["sSubKeyName"] = regKeyToGet;
            inParams["sValueName"] = keyToRead;

            ManagementBaseObject outParams = registry.InvokeMethod("GetStringValue", inParams, null);
            // MessageBox.Show(outParams["sValue"].ToString());
            toolStripStatusLabel1.Text = outParams["sValue"].ToString();

            if (toolStripStatusLabel1.Text == "0")
            {
                label2.ForeColor = System.Drawing.Color.Red;
                label2.Text = "Stitching Overlap is Disabled";
            }
            if (toolStripStatusLabel1.Text == "7112")
            {
                label2.ForeColor = System.Drawing.Color.Green;
                label2.Text = "Stitching Overlap is Enabled";
            }
            //else
            //{
            //    label2.ForeColor = System.Drawing.Color.Red;
            //    label2.Text = "Error Reading Registry";
            //}
            toolStripStatusLabel1.Text = "";
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            //stitchingStatus();
        }

        private void stitchingOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StitchOffset frm = new StitchOffset();
            frm.ShowDialog();
        }

        private void stitchOverlapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stitching frm = new Stitching();
            frm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Password frm = new Password();
            frm.ShowDialog();
        }
    }
}