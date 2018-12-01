using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Superweb_Restart_Application
{
    public partial class Stitching : Form
    {
        private XmlDocument doc;
        private XmlElement root;
        public string Descanso = @"C:\Test\DescansoInit.xml";

        public Stitching()
        {
            InitializeComponent();
        }

        private void Stitching_Load(object sender, EventArgs e)
        {
            CheckStitch();
            CheckReg();
        }

        public void CheckStitch()
        {
            XDocument xDoc = XDocument.Load(Descanso);
            label6.Text = xDoc.Descendants("stitchingEnabledSetting").First().Value;
        }

        public void CheckReg()
        {
            try
            {
                int i = 1;
                while (i < 4)
                {
                    Label[] lray = { label6, label7, label8, label9 };
                    foreach (Label label in lray)
                    {
                        string ServerName = ConfigurationManager.AppSettings.Get("A" + i);
                        string regKeyToGet = @"SOFTWARE\Wow6432Node\Memjet\Aspen\Controller";
                        string keyToRead = "StitchOverlapMicrons";

                        // Connection Login if needed

                        ConnectionOptions oConn = new ConnectionOptions();
                        //oConn.Username = "memjet";
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
                        label.Text = outParams["sValue"].ToString();

                        i++;
                    }
                }
                string aspen1 = label6.Text;
                string aspen2 = label7.Text;
                string aspen3 = label8.Text;
                string aspen4 = label9.Text;
                if (aspen1 == "0" && aspen2 == "0" && aspen3 == "0" && aspen4 == "0")
                {
                    button1.Text = "Enable Stitching";
                }
                else
                {
                    button1.Text = "Disable Stitching";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Enable Stitching")
                {
                    XDocument xDoc = XDocument.Load(Descanso);
                    xDoc.Descendants("stitchingEnabledSetting").First().Value = "True";
                    xDoc.Save(Descanso);

                    Cursor.Current = Cursors.WaitCursor;
                    int i = 1;
                    while (i < 5)
                    {
                        string Value = "7112";

                        //Change Registry Value to 7112

                        string ServerName = ConfigurationManager.AppSettings.Get("A" + i);
                        string regKeyToGet = @"SOFTWARE\Wow6432Node\Memjet\Aspen\Controller";
                        string keyToRead = "StitchOverlapMicrons";

                        // Connection Login if needed

                        ConnectionOptions oConn = new ConnectionOptions();
                        //oConn.Username = "memjet";
                        //oConn.Password = "memjet1";
                        System.Management.ManagementScope scope = new System.Management.ManagementScope(@"\\" + ServerName + @"\root\default", oConn);

                        scope.Options.EnablePrivileges = true;
                        scope.Connect();

                        ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
                        ManagementBaseObject inParams = registry.GetMethodParameters("CreateKey");
                        inParams["sSubKeyName"] = regKeyToGet;
                        ManagementBaseObject outParams = registry.InvokeMethod("CreateKey", inParams, null);

                        ManagementBaseObject inParams6 = registry.GetMethodParameters("SetStringValue");
                        inParams6["sSubKeyName"] = regKeyToGet;
                        inParams6["sValueName"] = keyToRead;
                        inParams6["sValue"] = Value;
                        ManagementBaseObject outParams6 = registry.InvokeMethod("SetStringValue", inParams6, null);

                        i++;
                    }


                    Cursor.Current = Cursors.Default;
                }
                else if (button1.Text == "Disable Stitching")
                {
                    XDocument xDoc = XDocument.Load(Descanso);
                    xDoc.Descendants("stitchingEnabledSetting").First().Value = "False";
                    xDoc.Save(Descanso);

                    Cursor.Current = Cursors.WaitCursor;
                    int i = 1;
                    while (i < 5)
                    {
                        string Value = "0";

                        //Change Registry Value to 0

                        string ServerName = ConfigurationManager.AppSettings.Get("A" + i);
                        string regKeyToGet = @"SOFTWARE\Wow6432Node\Memjet\Aspen\Controller";
                        string keyToRead = "StitchOverlapMicrons";

                        // Connection Login if needed

                        ConnectionOptions oConn = new ConnectionOptions();
                        //oConn.Username = "memjet";
                        //oConn.Password = "memjet1";
                        System.Management.ManagementScope scope = new System.Management.ManagementScope(@"\\" + ServerName + @"\root\default", oConn);

                        scope.Options.EnablePrivileges = true;
                        scope.Connect();

                        ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
                        ManagementBaseObject inParams = registry.GetMethodParameters("CreateKey");
                        // inParams["hDefKey"] = (UInt32)2147483650;
                        inParams["sSubKeyName"] = regKeyToGet;
                        ManagementBaseObject outParams = registry.InvokeMethod("CreateKey", inParams, null);

                        ManagementBaseObject inParams6 = registry.GetMethodParameters("SetStringValue");
                        //inParams6["hDefKey"] = 2147483650;
                        inParams6["sSubKeyName"] = regKeyToGet;
                        inParams6["sValueName"] = keyToRead;
                        inParams6["sValue"] = Value;
                        ManagementBaseObject outParams6 = registry.InvokeMethod("SetStringValue", inParams6, null);

                        i++;
                    }
                    Cursor.Current = Cursors.Default;
                }
                DialogResult result = MessageBox.Show("You need to restart the Aspen service on all 4 machines\nWould you like to do that now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ProgressDialog progressDialog = new ProgressDialog();
                    for (int n = 0; n < 100; n++)
                    {
                        progressDialog.UpdateProgress(n); // Update progress in progressDialog
                    }
                    progressDialog.ChangeLabel("Restarting: All Aspen Services");
                    progressDialog.Show();

                    restartAspen();

                    progressDialog.Close();
                    Cursor.Current = Cursors.Default;
                }
                CheckReg();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void restartAspen()
        {

            int i = 1;
            while (i < 5)

            {
                var serverName = ConfigurationManager.AppSettings.Get("Borrego " + i);
                string appPoolName = ".NET 4.Aspen";


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
                                i++;
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
        }
    }
}