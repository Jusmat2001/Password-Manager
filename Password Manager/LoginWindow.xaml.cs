using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Security;
using System.Security.Cryptography;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //if (!Properties.Settings.Default.Registered)
            //{
            //    FirstRun.Register();

            //}
            InitData();
        }

        private void InitData()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                if (Properties.Settings.Default.Remme == "yes")
                {
                    lwNameBox.Text = Properties.Settings.Default.UserName;
                    SecureString password = Crypt.DecryptString(Properties.Settings.Default.Password);
                    lwPassBox.Text = Crypt.ToInsecureString(password);
                    remmeCheckbox.IsChecked = true;
                }
                else
                {
                    lwNameBox.Text = Properties.Settings.Default.UserName;
                }
            }
        }

        private void SaveData()
        {
            if ((bool) remmeCheckbox.IsChecked)
            {
                Properties.Settings.Default.UserName = lwNameBox.Text;
                SecureString password = Crypt.ToSecureString(lwPassBox.Text);
                Properties.Settings.Default.Password = Crypt.EncryptString(password);
                Properties.Settings.Default.Remme = "yes";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = lwNameBox.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remme = "no";
                Properties.Settings.Default.Save();
            }
        }
        

        

        
    }
}
