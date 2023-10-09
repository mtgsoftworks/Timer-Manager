using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MTG_Timer_Manager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }     
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
            }
        }

        private void kullanımPaneliniAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.ShowDialog();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu Program Mesut Taha Güven Tarafından Yazılmıştır","MTG Timer Manager",MessageBoxButtons.OK,MessageBoxIcon.Information);
            MessageBox.Show("2019 © MTG Software", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {           
            if(Properties.Settings.Default.doğrulanma == true)
            {
                timer1.Start();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double veri1;
            veri1 = (Properties.Settings.Default.saat2 - Properties.Settings.Default.saat1) * 3600;

            double sayac;
            sayac = veri1;

            

            sayac -= 1;
            label2.Text = (sayac / 3600).ToString("0.")+"Saat "+(sayac%3600).ToString()+"Dakikanız Kaldı "+ ((sayac % 3600)%60).ToString()+"Saniyeniz Kaldı";
           

            if(veri1.ToString()==label2.Text)
            {
                timer1.Stop();
                MessageBox.Show("Kullanım Süreniz Doldu Bilgisayar Şimdi Kapanıyor", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 1");
            }
        }
    }
}
