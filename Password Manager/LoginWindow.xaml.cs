﻿using System;
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

        private MainWindow m_parent;

        public LoginWindow(MainWindow parent):this()
        {
            m_parent = parent;
            InitializeComponent();
            //Use to reset app data for debugging 
            //Properties.Settings.Default.Reset();
            InitData();
        }

        public LoginWindow()
        {
        }

        private void InitData()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                if (Properties.Settings.Default.Remme == "yes")//if remember me checked, it will autofill crypted password. if not just username
                {
                    lwNameBox.Text = Properties.Settings.Default.UserName;
                    SecureString password = Crypt.DecryptString(Properties.Settings.Default.Password);
                    lwPassBox.Password = Crypt.ToInsecureString(password);
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
            if ((bool) remmeCheckbox.IsChecked)//if checked, saves username and password and encrypts pass. 
            {
                Properties.Settings.Default.UserName = lwNameBox.Text;
                SecureString password = Crypt.ToSecureString(lwPassBox.Password);
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

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sUser = lwNameBox.Text;
            string sPass = lwPassBox.Password;

            //check for valid credentials and log in or dont
            if (SQLAccess.LoginCheck(sUser, sPass))
            {
                SaveData();
                MainWindow.sUsername = sUser;
                
                this.Close();
                m_parent.Show();
                
            }
            else
            {
                MessageBox.Show("Login info does not match. Please try again or email Justin in IT if the problem persists. ");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.FirstRun)//if first run setting is true, load first run window
            {
                
                this.Hide();
                FirstRun fr = new FirstRun(this);
                fr.Show();
            }
        }

        
    }
}
