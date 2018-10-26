using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private DateTime dtregDate;


        public FirstRun()
        {
            InitializeComponent();


        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fNameBox.Text == string.Empty || lNameBox.Text == string.Empty || passBox.Password == string.Empty)
                {
                    MessageBox.Show("Please fill out all fields to continue.");
                    
                }
                if (fNameBox.Text != string.Empty && lNameBox.Text != string.Empty && passBox.Password != string.Empty)
                {
                    dtregDate = DateTime.Now;
                    sfirstname = fNameBox.Text.ToLower();
                    slastname = lNameBox.Text.ToLower();
                    spass = passBox.Password.ToLower();
                    susername = sfirstname[0] + slastname;
                    if (DupCheck(susername))
                    {
                        MessageBox.Show("Duplicate user. See Justin in IT.");
                        Environment.Exit(0);
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public bool DupCheck(string susername)
        {
            string sQuery = "SELECT @susername FROM [dbo].[PMUsers];";
            using (SqlConnection connection = new SqlConnection(SQLAccess.ConnVal("C1user")))
            {
                var command = new SqlCommand(sQuery, connection);
                command.Parameters.AddWithValue("@susername", susername);
                connection.Open();
                string result = (string) command.ExecuteScalar();
                if (result == null)
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
        }
    }
}
