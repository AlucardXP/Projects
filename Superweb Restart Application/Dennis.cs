using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Superweb_Restart_Application
{
    public partial class Dennis : Form
    {
        private SoundPlayer Player = new SoundPlayer();

        public Dennis()
        {
            InitializeComponent();
        }

        private void playaudio() // defining the function
        {
            SoundPlayer audio = new SoundPlayer(Superweb_Restart_Application.Properties.Resources.magicword); 
            audio.Play();
            audio.PlayLooping();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;
            this.Player.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Player.Stop();
            this.Close();

        }

        private void Dennis_Load(object sender, EventArgs e)
        {
            playaudio();
        }
    }
}
