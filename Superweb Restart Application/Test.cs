using System;
using Microsoft.Win32;
using System.Windows.Forms;
using Microsoft.Web.Management.Server;
using System.Management;
using System.Threading;
using System.ComponentModel;
using System.Net;

namespace Superweb_Restart_Application
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GetA1Name("192.168.130.11");
            //GetA2Name("192.168.130.12");
            //GetA3Name("192.168.130.13");
            //GetA4Name("192.168.130.14");
        }
        private void testFunction()
        {
            ConnectionOptions oConn = new ConnectionOptions();
            string hostname = "";
            System.Management.ManagementScope scope = new System.Management.ManagementScope(@"\\" + hostname + @"\root\cimv2", oConn);

            scope.Connect();
            ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
            ManagementBaseObject inParams = registry.GetMethodParameters("GetStringValue");
            inParams["hDefKey"] = 0x80000002; // HKEY_LOCAL_MACHINE;
            inParams["sSubKeyName"] = "SOFTWARE\\Microsoft\\.NETFramework";
            inParams["sValueName"] = "InstallRoot";


            ManagementBaseObject outParams = registry.InvokeMethod("GetStringValue", inParams, null);

            if (outParams.Properties["sValue"].Value != null)
            {
               // output = outParams.Properties["sValue"].Value.ToString();

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
                MessageBox.Show("Machine NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label9.Text = machineName;
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
                MessageBox.Show("Machine NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label10.Text = machineName;
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
                MessageBox.Show("Machine NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label11.Text = machineName;
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
                MessageBox.Show("Machine NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label12.Text = machineName;
        }
    }
}