using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Superweb_Restart_Application
{
    public partial class StitchOffset : Form
    {
        private XmlDocument doc;
        private XmlElement root;
        public string PATH1 = @"C:\Test\BorregoInit1.xml";
        public string PATH2 = @"C:\Test\BorregoInit2.xml";
        public string PATH3 = @"C:\Test\BorregoInit3.xml";
        public string PATH4 = @"C:\Test\BorregoInit4.xml";

        public StitchOffset()
        {
            InitializeComponent();
        }

        private void StitchOffset_Load(object sender, EventArgs e)
        {
            LoadXML1();
            LoadXML2();
            LoadXML3();
            LoadXML4();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadXML1()
        {
            XDocument xDoc = XDocument.Load(PATH1);
            label6.Text = xDoc.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value;
        }

        private void LoadXML2()
        {
            XDocument xDoc = XDocument.Load(PATH2);
            label7.Text = xDoc.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value;
        }

        private void LoadXML3()
        {
            XDocument xDoc = XDocument.Load(PATH3);
            label8.Text = xDoc.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value;
        }

        private void LoadXML4()
        {
            XDocument xDoc = XDocument.Load(PATH4);
            label9.Text = xDoc.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value;
        }

        private void SetEnginesBtn_Click(object sender, EventArgs e)
        {
            SetEngines();
        }

        private void SetEngines()
        {
            XDocument xDoc1 = XDocument.Load(PATH1);
            XDocument xDoc2 = XDocument.Load(PATH2);
            XDocument xDoc3 = XDocument.Load(PATH3);
            XDocument xDoc4 = XDocument.Load(PATH4);

            string Engine2 = label6.Text;
            string Engine4 = label7.Text;

            xDoc2.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value = Engine2;
            xDoc4.Descendants("OEMEncoderTicksPerThousandInchesSetting").First().Value = Engine4;

            xDoc2.Save(PATH2);
            xDoc4.Save(PATH4);

            LoadXML1();
            LoadXML2();
            LoadXML3();
            LoadXML4();

        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            RestartAspen();
        }

        private void RestartAspen()
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
