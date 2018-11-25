using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Threading;
using System.Text;
using Microsoft.Web.Administration;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace Superweb_Restart_Application
{
    public partial class Status : Form
    {
        public Status()
        {
            InitializeComponent();
            //startPing(); - startPing is now loaded through progressBar()
            progressBar();
        }

        private void Status_Init(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar();
        }

        private void startPing()
        {
            // Check Moby/Gymea configuration
            string moby1 = null;
            string moby2 = null;
            string moby3 = null;
            string moby4 = null;

            string checkvalue = ConfigurationManager.AppSettings["qamk_check"];
            string checkvalue1 = ConfigurationManager.AppSettings["Gymea_Check"];

            if (checkvalue == "1" && checkvalue1 == "0")
            {
                groupBox2.Text = "Moby Status (QAMK)";
                moby1 = "Moby 1";
                moby2 = "Moby 2";
                moby3 = "Moby 3";
                moby4 = "Moby 4";
            }
            else if (checkvalue1 == "1" && checkvalue == "0")
            {
                groupBox2.Text = "Gymea Status";
                label16.Text = "Gymea 1";
                label15.Text = "Gymea 2";
                label14.Text = "Gymea 3";
                label13.Text = "Gymea 4";
                moby1 = "Gymea 1";
                moby2 = "Gymea 2";
                moby3 = "Gymea 3";
                moby4 = "Gymea 4";
            }
            else if (checkvalue == "1" && checkvalue1 == "1")
            {
                groupBox2.Text = "Gymea Status (QAMK)";
                label16.Text = "Gymea 1";
                label15.Text = "Gymea 2";
                label14.Text = "Gymea 3";
                label13.Text = "Gymea 4";
                moby1 = "Gymea 1 QAMK";
                moby2 = "Gymea 2 QAMK";
                moby3 = "Gymea 3 QAMK";
                moby4 = "Gymea 4 QAMK";
            }
            else if (checkvalue == "0" && checkvalue1 == "0")
            {
                groupBox2.Text = "Moby Status (RIT)";
                moby1 = "Moby 1 RIT";
                moby2 = "Moby 2 RIT";
                moby3 = "Moby 3 RIT";
                moby4 = "Moby 4 RIT";
            }

            //Ping IP Addresses
            Ping ping = new Ping();
            AutoResetEvent waiter = new AutoResetEvent(false);
            string data = "aaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1;
            PingOptions options = new PingOptions(1, true);

            // Boreggos
            var pingBoreggo1 = ping.Send(ConfigurationManager.AppSettings.Get("Borrego 1"), timeout, buffer, options);
            var pingBoreggo2 = ping.Send(ConfigurationManager.AppSettings.Get("Borrego 2"), timeout, buffer, options);
            var pingBoreggo3 = ping.Send(ConfigurationManager.AppSettings.Get("Borrego 3"), timeout, buffer, options);
            var pingBoreggo4 = ping.Send(ConfigurationManager.AppSettings.Get("Borrego 4"), timeout, buffer, options);

            // Xitron Rips
            var pingXitron1 = ping.Send(ConfigurationManager.AppSettings.Get("Rip 1"), timeout, buffer, options);
            var pingXitron2 = ping.Send(ConfigurationManager.AppSettings.Get("Rip 2"), timeout, buffer, options);
            var pingXitron3 = ping.Send(ConfigurationManager.AppSettings.Get("Rip 3"), timeout, buffer, options);
            var pingXitron4 = ping.Send(ConfigurationManager.AppSettings.Get("Rip 4"), timeout, buffer, options);

            // Mobys / GYMEA Ping
            if (checkvalue == "1" && checkvalue1 == "0")
            {
                var pingMoby1 = ping.Send(ConfigurationManager.AppSettings.Get(moby1), timeout, buffer, options);
                var pingMoby2 = ping.Send(ConfigurationManager.AppSettings.Get(moby2), timeout, buffer, options);
                var pingMoby3 = ping.Send(ConfigurationManager.AppSettings.Get(moby3), timeout, buffer, options);
                var pingMoby4 = ping.Send(ConfigurationManager.AppSettings.Get(moby4), timeout, buffer, options);

                // M1 Lights
                if (pingMoby1.Status == IPStatus.Success)
                {
                    label12.ForeColor = System.Drawing.Color.Green;
                    label12.Text = "ONLINE";
                }
                else
                {
                    label12.ForeColor = System.Drawing.Color.Red;
                    label12.Text = "OFFLINE";
                }

                // M2 Lights
                if (pingMoby2.Status == IPStatus.Success)
                {
                    label11.ForeColor = System.Drawing.Color.Green;
                    label11.Text = "ONLINE";
                }
                else
                {
                    label11.ForeColor = System.Drawing.Color.Red;
                    label11.Text = "OFFLINE";
                }

                // M3 Lights
                if (pingMoby3.Status == IPStatus.Success)
                {
                    label10.ForeColor = System.Drawing.Color.Green;
                    label10.Text = "ONLINE";
                }
                else
                {
                    label10.ForeColor = System.Drawing.Color.Red;
                    label10.Text = "OFFLINE";
                }

                // M4 Lights
                if (pingMoby4.Status == IPStatus.Success)
                {
                    label9.ForeColor = System.Drawing.Color.Green;
                    label9.Text = "ONLINE";
                }
                else
                {
                    label9.ForeColor = System.Drawing.Color.Red;
                    label9.Text = "OFFLINE";
                }

            }
            else if (checkvalue == "0" && checkvalue1 == "0")
            {
                if (pingBoreggo1.Status == IPStatus.Success)
                {
                    pingRIT1();
                }
                if (pingBoreggo2.Status == IPStatus.Success)
                {
                    pingRIT2();
                }
                if (pingBoreggo3.Status == IPStatus.Success)
                {
                    pingRIT3();
                }
                if (pingBoreggo4.Status == IPStatus.Success)
                {
                    pingRIT4();
                }
            }
            else if (checkvalue == "1" && checkvalue1 == "0")
            {
                if (pingBoreggo1.Status == IPStatus.Success)
                {
                    pingGYMEA1();
                }
                if (pingBoreggo2.Status == IPStatus.Success)
                {
                    pingGYMEA2();
                }
                if (pingBoreggo3.Status == IPStatus.Success)
                {
                    pingGYMEA3();
                }
                if (pingBoreggo4.Status == IPStatus.Success)
                {
                    pingGYMEA4();
                }
            }
            else if (checkvalue == "1" && checkvalue1 == "1")
            {
                if (pingBoreggo1.Status == IPStatus.Success)
                {
                    pingGYMEA1();
                }
                if (pingBoreggo2.Status == IPStatus.Success)
                {
                    pingGYMEA2();
                }
                if (pingBoreggo3.Status == IPStatus.Success)
                {
                    pingGYMEA3();
                }
                if (pingBoreggo4.Status == IPStatus.Success)
                {
                    pingGYMEA4();
                }
            }

            // Write Status in Label Text
            // Boreggo Lights
            //B1
            if (pingBoreggo1.Status == IPStatus.Success)
            {
                label5.ForeColor = System.Drawing.Color.Green;
                label5.Text = "ONLINE";
            }
            else
            {
                label5.ForeColor = System.Drawing.Color.Red;
                label5.Text = "OFFLINE";
            }
            //B2
            if (pingBoreggo2.Status == IPStatus.Success)
            {
                label6.ForeColor = System.Drawing.Color.Green;
                label6.Text = "ONLINE";
            }
            else
            {
                label6.ForeColor = System.Drawing.Color.Red;
                label6.Text = "OFFLINE";
            }
            //B3
            if (pingBoreggo3.Status == IPStatus.Success)
            {
                label7.ForeColor = System.Drawing.Color.Green;
                label7.Text = "ONLINE";
            }
            else
            {
                label7.ForeColor = System.Drawing.Color.Red;
                label7.Text = "OFFLINE";
            }
            // B4
            if (pingBoreggo4.Status == IPStatus.Success)
            {
                label8.ForeColor = System.Drawing.Color.Green;
                label8.Text = "ONLINE";
            }
            else
            {
                label8.ForeColor = System.Drawing.Color.Red;
                label8.Text = "OFFLINE";
            }

            // Xitron Lights
            // X1
            if (pingXitron1.Status == IPStatus.Success)
            {
                label20.ForeColor = System.Drawing.Color.Green;
                label20.Text = "ONLINE";
            }
            else
            {
                label20.ForeColor = System.Drawing.Color.Red;
                label20.Text = "OFFLINE";
            }
            // X2
            if (pingXitron2.Status == IPStatus.Success)
            {
                label19.ForeColor = System.Drawing.Color.Green;
                label19.Text = "ONLINE";
            }
            else
            {
                label19.ForeColor = System.Drawing.Color.Red;
                label19.Text = "OFFLINE";
            }
            // X3
            if (pingXitron3.Status == IPStatus.Success)
            {
                label18.ForeColor = System.Drawing.Color.Green;
                label18.Text = "ONLINE";
            }
            else
            {
                label18.ForeColor = System.Drawing.Color.Red;
                label18.Text = "OFFLINE";
            }

            // X4
            if (pingXitron4.Status == IPStatus.Success)
            {
                label17.ForeColor = System.Drawing.Color.Green;
                label17.Text = "ONLINE";
            }
            else
            {
                label17.ForeColor = System.Drawing.Color.Red;
                label17.Text = "OFFLINE";
            }

            // IIS Services
            pingAspen1();
            pingAspen2();
            pingAspen3();
            pingAspen4();
            pingCedar();
            pingSC();
        }

        private void pingRIT1()
        {
            string aspen = ConfigurationManager.AppSettings.Get("A1");
            string filePath = @"c:\putty\psexec.exe";
            if (File.Exists(filePath))
            {
                // Start a new process which open a command prompt 
                {

                    Process processInfo = new Process();
                    processInfo.StartInfo.FileName = filePath;
                    processInfo.StartInfo.Arguments = @"\\" + aspen + @" C:\putty\pingmoby.bat";
                    processInfo.StartInfo.UseShellExecute = false;
                    processInfo.StartInfo.CreateNoWindow = true;
                    processInfo.StartInfo.RedirectStandardError = true;
                    processInfo.StartInfo.RedirectStandardOutput = true;
                    processInfo.StartInfo.RedirectStandardInput = true;
                    processInfo.Start();

                    processInfo.WaitForExit();

                    if (processInfo.ExitCode.ToString() == "0")
                    {
                        label12.ForeColor = System.Drawing.Color.Green;
                        label12.Text = "ONLINE";
                    }
                    else
                    {
                        label12.ForeColor = System.Drawing.Color.Red;
                        label12.Text = "OFFLINE";
                    }
                }
            }
        }

        private void pingRIT2()
        {
            string aspen = ConfigurationManager.AppSettings.Get("A2");
            string filePath = @"c:\putty\psexec.exe";
            if (File.Exists(filePath))
            {
                // Start a new process which open a command prompt 
                {

                    Process processInfo = new Process();
                    processInfo.StartInfo.FileName = filePath;
                    processInfo.StartInfo.Arguments = @"\\" + aspen + @" C:\putty\pingmoby.bat";
                    processInfo.StartInfo.UseShellExecute = false;
                    processInfo.StartInfo.CreateNoWindow = true;
                    processInfo.StartInfo.RedirectStandardError = true;
                    processInfo.StartInfo.RedirectStandardOutput = true;
                    processInfo.StartInfo.RedirectStandardInput = true;
                    processInfo.Start();

                    processInfo.WaitForExit();

                    if (processInfo.ExitCode.ToString() == "0")
                    {
                        label11.ForeColor = System.Drawing.Color.Green;
                        label11.Text = "ONLINE";
                    }
                    else
                    {
                        label11.ForeColor = System.Drawing.Color.Red;
                        label11.Text = "OFFLINE";
                    }
                }
            }
        }

        private void pingRIT3()
        {
            string aspen = ConfigurationManager.AppSettings.Get("A3");
            string filePath = @"c:\putty\psexec.exe";
            if (File.Exists(filePath))
            {
                // Start a new process which open a command prompt 
                {

                    Process processInfo = new Process();
                    processInfo.StartInfo.FileName = filePath;
                    processInfo.StartInfo.Arguments = @"\\" + aspen + @" C:\putty\pingmoby.bat";
                    processInfo.StartInfo.UseShellExecute = false;
                    processInfo.StartInfo.CreateNoWindow = true;
                    processInfo.StartInfo.RedirectStandardError = true;
                    processInfo.StartInfo.RedirectStandardOutput = true;
                    processInfo.StartInfo.RedirectStandardInput = true;
                    processInfo.Start();

                    processInfo.WaitForExit();

                    if (processInfo.ExitCode.ToString() == "0")
                    {
                        label10.ForeColor = System.Drawing.Color.Green;
                        label10.Text = "ONLINE";
                    }
                    else
                    {
                        label10.ForeColor = System.Drawing.Color.Red;
                        label10.Text = "OFFLINE";
                    }
                }
            }
        }

        private void pingRIT4()
        {
            // Throw something together that will remote to each boreggo and ping RIT Mobys
            // 131.41, 132.41, 133.41, 134.41
            // Remote cmd, run ping, capture results
            string aspen = ConfigurationManager.AppSettings.Get("A4");
            string filePath = @"c:\putty\psexec.exe";
            if (File.Exists(filePath))
            {
                // Start a new process which open a command prompt 
                {

                    Process processInfo = new Process();
                    processInfo.StartInfo.FileName = filePath;
                    processInfo.StartInfo.Arguments = @"\\" + aspen + @" C:\putty\pingmoby.bat";
                    processInfo.StartInfo.UseShellExecute = false;
                    processInfo.StartInfo.CreateNoWindow = true;
                    processInfo.StartInfo.RedirectStandardError = true;
                    processInfo.StartInfo.RedirectStandardOutput = true;
                    processInfo.StartInfo.RedirectStandardInput = true;
                    processInfo.Start();

                    processInfo.WaitForExit();

                    if (processInfo.ExitCode.ToString() == "0")
                    {
                        label9.ForeColor = System.Drawing.Color.Green;
                        label9.Text = "ONLINE";
                    }
                    else
                    {
                        label9.ForeColor = System.Drawing.Color.Red;
                        label9.Text = "OFFLINE";
                    }
                }
            }
        }

        private void pingGYMEA1()
        {
            // Similar to ping RIT, we will need to pick each block of Gymeas from the Boreggo
        }

        private void pingGYMEA2()
        {
            // Similar to ping RIT, we will need to pick each block of Gymeas from the Boreggo
        }

        private void pingGYMEA3()
        {
            // Similar to ping RIT, we will need to pick each block of Gymeas from the Boreggo
        }

        private void pingGYMEA4()
        {
            // Similar to ping RIT, we will need to pick each block of Gymeas from the Boreggo
        }

        private void pingAspen1()
        {
            //var ip = "192.168.130.11";
            //try
            //{
            //    if (label5.Text == "ONLINE")
            //    {
            //        ServerManager serverManager = ServerManager.OpenRemote(ip);
            //        Site aspen = serverManager.Sites["Aspen"];

            //        // get the app for this site
            //        var appName = aspen.Applications[0].ApplicationPoolName;
            //        ApplicationPool appPool = serverManager.ApplicationPools[appName];

            //        if (appPool.State == ObjectState.Started)
            //        {
            //            label31.ForeColor = System.Drawing.Color.Green;
            //            label31.Text = "ONLINE";
            //        }
            //        else if (appPool.State == ObjectState.Stopped)
            //        {
            //            label31.ForeColor = System.Drawing.Color.Red;
            //            label31.Text = "OFFLINE";
            //        }
            //    }
            //    else
            //    {
            //        label31.ForeColor = System.Drawing.Color.Red;
            //        label31.Text = "OFFLINE";
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //}


            try
            {
                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A1");
                string strAppPool = "Aspen";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label31.ForeColor = System.Drawing.Color.Red;
                            label31.Text = "OFFLINE";
                        }
                    }
                }
                label31.ForeColor = System.Drawing.Color.Green;
                label31.Text = "ONLINE";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void pingAspen2()
        {
            try
            {
                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A2");
                string strAppPool = "Aspen";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label32.ForeColor = System.Drawing.Color.Red;
                            label32.Text = "OFFLINE";
                        }
                    }
                }
                label32.ForeColor = System.Drawing.Color.Green;
                label32.Text = "ONLINE";
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void pingAspen3()
        {
            try
            {

                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A3");
                string strAppPool = "Aspen";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label33.ForeColor = System.Drawing.Color.Red;
                            label33.Text = "OFFLINE";
                        }
                    }
                }
                label33.ForeColor = System.Drawing.Color.Green;
                label33.Text = "ONLINE";
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void pingAspen4()
        {
            try
            {
                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A4");
                string strAppPool = "Aspen";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label34.ForeColor = System.Drawing.Color.Red;
                            label34.Text = "OFFLINE";
                        }
                    }
                }
                label34.ForeColor = System.Drawing.Color.Green;
                label34.Text = "ONLINE";
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void pingCedar()
        {
            try
            {
                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A4");
                string strAppPool = "Cedar";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label35.ForeColor = System.Drawing.Color.Red;
                            label35.Text = "OFFLINE";
                        }
                    }
                }
                label35.ForeColor = System.Drawing.Color.Green;
                label35.Text = "ONLINE";
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void pingSC()
        {
            try
            {
                //string sb = ""; // String to store return value
                string servername = ConfigurationManager.AppSettings.Get("A4");
                string strAppPool = "SuperCloud";

                // Connection options for WMI object
                ConnectionOptions options = new ConnectionOptions();

                // Packet Privacy means authentication with encrypted connection.
                options.Authentication = AuthenticationLevel.PacketPrivacy;

                // EnablePrivileges : Value indicating whether user privileges 
                // need to be enabled for the connection operation. 
                // This property should only be used when the operation performed 
                // requires a certain user privilege to be enabled.
                options.EnablePrivileges = true;

                // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                ManagementScope scope = new ManagementScope(@"\\" +
                    servername + "\\root\\MicrosoftIISv2", options);

                // Query IIS WMI property IISApplicationPoolSetting
                ObjectQuery oQueryIISApplicationPoolSetting =
                    new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

                // Search and collect details thru WMI methods
                ManagementObjectSearcher moSearcherIISApplicationPoolSetting =
                    new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
                ManagementObjectCollection collectionIISApplicationPoolSetting =
                                moSearcherIISApplicationPoolSetting.Get();

                // Loop thru every object
                foreach (ManagementObject resIISApplicationPoolSetting
                    in collectionIISApplicationPoolSetting)
                {
                    // IISApplicationPoolSetting has a property called Name which will 
                    // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                    // Extract Application Pool Name alone using Split()
                    if (resIISApplicationPoolSetting
                        ["Name"].ToString().Split('/')[2] == strAppPool)
                    {
                        // IISApplicationPoolSetting has a property 
                        // called AppPoolState which has following values
                        // 2 = started 4 = stopped 1 = starting 3 = stopping
                        if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                        {
                            label36.ForeColor = System.Drawing.Color.Red;
                            label36.Text = "OFFLINE";
                        }
                    }
                }
                label36.ForeColor = System.Drawing.Color.Green;
                label36.Text = "ONLINE";
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private bool isProcessRunning = false;
        private void progressBar()
        {
            try
            {
                if (isProcessRunning)
                {
                    MessageBox.Show("A process is already running.");
                    return;
                }

                ProgressDialog progressDialog = new ProgressDialog();

                Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        isProcessRunning = true;

                        for (int n = 0; n < 100; n++)
                        {
                            progressDialog.ChangeLabel("Pinging IP Addresses");
                            Thread.Sleep(50);
                            progressDialog.UpdateProgress(n); // Update progress in progressDialog
                    }
                        startPing();
                    // MessageBox.Show("Thread completed!");
                    // No need to reset the progress since we are closing the dialog
                    progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                        isProcessRunning = false;
                    }
                ));
                backgroundThread.Start();

                progressDialog.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
    }
}