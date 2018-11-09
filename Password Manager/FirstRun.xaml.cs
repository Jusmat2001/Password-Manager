using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for FirstRun.xaml
    /// </summary>
    public partial class FirstRun : Window
    {
        private string sfirstname, slastname, susername, spass;
        private LoginWindow l_parent;
        private bool safeclose = false;

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!safeclose)
            {
                Application.Current.Shutdown();
            }
        }

        public FirstRun()
        {
        }

        public FirstRun(LoginWindow parent):this()
        {
            l_parent = parent;
            InitializeComponent();

            l_parent.Visibility = Visibility.Collapsed;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fNameBox.Text == string.Empty || lNameBox.Text == string.Empty || passBox.Password == string.Empty)
                {
                    MessageBox.Show("Please fill out all fields to continue.");
                    return;
                }
                if (fNameBox.Text != string.Empty && lNameBox.Text != string.Empty && passBox.Password != string.Empty)
                {
                    sfirstname = fNameBox.Text.ToLower();
                    slastname = lNameBox.Text.ToLower();
                    spass = passBox.Password.ToLower();
                    susername = sfirstname[0] + slastname;
                    
                    SQLAccess.AddUser(susername, spass);
                    MessageBox.Show("Your username is : " + susername + "\nAnd your password is : " + spass + "\n\nPlease log in.");
                    Properties.Settings.Default.FirstRun = false;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duplicate User Registered. Please email Justin in IT.\n\n"+ex.Message);
                return;
            }
            safeclose = true;
            this.Close();
            l_parent.Show();
        }
    }
}
    

