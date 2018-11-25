using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Superweb_Restart_Application
{
    public partial class StitchOffset : Form
    {
        private XmlDocument doc;
        private XmlElement root;
        public string PATH1 = @"C:\Test\BorregoInit.xml";
        public string PATH2 = @"C:\Test\BorregoInit.xml";
        public string PATH3 = @"C:\Test\BorregoInit.xml";
        public string PATH4 = @"C:\Test\BorregoInit.xml";

        public StitchOffset()
        {
            InitializeComponent();
        }

        private void StitchOffset_Load(object sender, EventArgs e)
        {
            loadXML1();
            loadXML2();
            loadXML3();
            loadXML4();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadXML1()
        {
            doc = new XmlDocument();
            doc.Load(PATH1);
            root = doc.DocumentElement;
            label6.Text = root.GetElementsByTagName("OEMEncoderTicksPerThousandInchesSetting")[0].InnerText;
        }

        private void loadXML2()
        {
            doc = new XmlDocument();
            doc.Load(PATH2);
            root = doc.DocumentElement;
            label7.Text = root.GetElementsByTagName("OEMEncoderTicksPerThousandInchesSetting")[0].InnerText;
        }

        private void loadXML3()
        {
            doc = new XmlDocument();
            doc.Load(PATH3);
            root = doc.DocumentElement;
            label8.Text = root.GetElementsByTagName("OEMEncoderTicksPerThousandInchesSetting")[0].InnerText;
        }

        private void loadXML4()
        {
            doc = new XmlDocument();
            doc.Load(PATH4);
            root = doc.DocumentElement;
            label9.Text = root.GetElementsByTagName("OEMEncoderTicksPerThousandInchesSetting")[0].InnerText;
        }
    }
}
