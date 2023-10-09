using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MTG_Timer_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (Properties.Settings.Default.kullanımsayısı != 0 & maskedTextBox1.Text != string.Empty & maskedTextBox2.Text != string.Empty & dateTimePicker1.Value != null & dateTimePicker2.Value != null & textBox1.Text != string.Empty)
            {
                Random rnd = new Random();
                int randomsayı = rnd.Next(100000, 1000000);
                linkLabel1.Text = randomsayı.ToString();

                Properties.Settings.Default.anaşifre = textBox1.Text;
                Properties.Settings.Default.saat1 = Convert.ToInt32(maskedTextBox1.Text);
                Properties.Settings.Default.saat2 = Convert.ToInt32(maskedTextBox2.Text);
                Properties.Settings.Default.tarih2 = dateTimePicker2.Value.ToString(); ;
                Properties.Settings.Default.kullanımsayısı = Convert.ToInt16(textBox2.Text);
                Properties.Settings.Default.tarih1 = dateTimePicker1.Value.ToString();
                Properties.Settings.Default.kod = linkLabel1.Text;
                Properties.Settings.Default.Save();
            }
            else
                {
                MessageBox.Show("Lütfen Tüm Kutuları Doldurun");
                Properties.Settings.Default.Reset();
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            MessageBox.Show("Şifreniz Kaydedildi","MTG Timer Manager",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
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
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ///Bilgisyar Açılınca Programı Otomatik Açma Komutları
            Properties.Settings.Default.aktiflik = false;         
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("MTG Timer Manager");
                key.Close();

                RegistryKey rkey1 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);

                rkey1.CreateSubKey("System", RegistryKeyPermissionCheck.Default);

                rkey1.Close();

                RegistryKey rkey2 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);

                rkey2.SetValue("DisableTaskMgr", 1);

                rkey2.Close();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.aktiflik == true)
            {
                radioButton1.Checked = true;
            }
            else
            {
                if (Properties.Settings.Default.aktiflik == false)
                {
                    radioButton2.Checked = true;
                }
            }
        }
    }
    }

