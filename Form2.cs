using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Microsoft.Win32;
using System.Reflection;
using System.Security.Principal;
using System.Windows;


namespace MTG_Timer_Manager
{
    public partial class Form2 : Form
    {
        private void AdminRelauncher()
        {        
            
        }
        public Form2()
        {
            InitializeComponent();
            Yoneticizni();
        }
        /// <summary>
        /// Form Hareket Ettirme Komutları
        /// </summary>
        private bool durum = false;
        private Point noktalar = Point.Empty;
        private void frmGiris_MouseDown(object sender, MouseEventArgs e)
        {
            Form frm = sender as Form;
            durum = true;
            frm.Cursor = Cursors.SizeAll;
            noktalar = e.Location;
        }

        private void frmGiris_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                Form frm = sender as Form;
                frm.Left = frm.Left + (e.X - noktalar.X);
                frm.Top = frm.Top + (e.Y - noktalar.Y);
            }
        }

        private void frmGiris_MouseUp(object sender, MouseEventArgs e)
        {
            Form frm = sender as Form;
            durum = false;
            frm.Cursor = Cursors.Default;
        }
        private void Yoneticizni()
        {
            if (!Yoneticiznikontrol())
            {
                ProcessStartInfo program = new ProcessStartInfo();
                program.UseShellExecute = true;
                program.WorkingDirectory = Environment.CurrentDirectory;
                program.FileName = Assembly.GetEntryAssembly().CodeBase;

                program.Verb = "runas";

                try
                {
                    Process.Start(program);
                    Environment.Exit(0);



                }
                catch (Exception)
                {
                    cevap = MessageBox.Show("Program yönetici izni olmadan düzgün çalışmayacaktır!\n yinede çalıştırmak istiyor musunuz ? ", "YÖNETİCİ İZNİ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (cevap == DialogResult.No)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {

                    }
                }
            }
        }
        DialogResult cevap;
        private bool Yoneticiznikontrol()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }



        int sayac;       
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac -= 1;
            if(sayac ==0)
            {
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 1");
                timer1.Stop();
            }
            label4.Text = sayac.ToString();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm1 = new Form1();
            string password = Convert.ToString(Interaction.InputBox("Lütfen Şifenizi Girin",
            "MTG Timer Mnaager"));
            if (Properties.Settings.Default.anaşifre != string.Empty)
            {

                if (Properties.Settings.Default.anaşifre == password)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                if (Properties.Settings.Default.anaşifre == string.Empty)
                {
                    frm1.ShowDialog();
                }
            }
                if (Properties.Settings.Default.anaşifre != password & Properties.Settings.Default.anaşifre != string.Empty)
                {
                    MessageBox.Show("Şifre Hatalı Lütfen Tekrar Deneyin", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }        
        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime tarih1 = Convert.ToDateTime(Properties.Settings.Default.tarih1);
            DateTime tarih2 = Convert.ToDateTime(Properties.Settings.Default.tarih2);
            TimeSpan ts = tarih2 - tarih1;
           

            if (Properties.Settings.Default.kod == textBox1.Text & Properties.Settings.Default.kullanımsayısı > 0 & ts.TotalHours>0)
            {
                label3.Text = "Doğrulandı";
                Properties.Settings.Default.doğrulanma = true;
                this.Hide();
                Properties.Settings.Default.kullanımsayısı -= 1;
            }
            else
            {
                if (Properties.Settings.Default.kod != textBox1.Text)
                {
                    MessageBox.Show("Hatalı Kod Girdiniz", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }
                if (Properties.Settings.Default.kullanımsayısı == 0 & textBox1.Text == Properties.Settings.Default.kod)
                {
                    MessageBox.Show("Kodun Kullanım Hakkı Bitmiştir", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();
                }
                if (Properties.Settings.Default.kod == textBox1.Text)
                {
                    
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
       {
            Properties.Settings.Default.aktiflik = true;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("MTG Timer Manager", "\"" + Application.ExecutablePath + "\"");
            key.Close();

            RegistryKey rkey1 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);

            rkey1.CreateSubKey("System", RegistryKeyPermissionCheck.Default);

            rkey1.Close();

            RegistryKey rkey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);

            rkey2.SetValue("DisableTaskMgr", 0);

            rkey2.Close();





            Properties.Settings.Default.doğrulanma = false;
            sayac = 80;
            timer1.Interval = 1000;                   
            timer1.Start();                      
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm1 = new Form1();
            string password = Convert.ToString(Interaction.InputBox("Lütfen Şifenizi Girin",
              "MTG Timer Mnaager"));

            if (Properties.Settings.Default.anaşifre != string.Empty & Properties.Settings.Default.anaşifre == password)
            {
                frm1.ShowDialog();
            }
            else
            {
                if (Properties.Settings.Default.anaşifre == string.Empty)
                {
                    frm1.ShowDialog();
                }
                else
                {
                    if (Properties.Settings.Default.anaşifre != password)
                    {
                        MessageBox.Show("Şifre Hatalı Lütfen Tekrar Deneyin", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(label3.Text =="Doğrulandı")
            {
                this.Hide();
            }
            else
            {
                if(label3.Text =="Doğrulanamdı")
                {
                    MessageBox.Show("Lütfen Kodunuzu Doğrulayın", "MTG Timer Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


