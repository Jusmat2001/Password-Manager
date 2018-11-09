using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Configuration;
using System.Data.Linq;
using System.Drawing;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string sMode = "user";
        public static string sUsername = "TBD";
        public static bool bIsSuper = false;

        cUser cU = new cUser() { User = sUsername };


        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            LoginWindow lw = new LoginWindow(this);
            lw.Show();
        }

        private void modeSwitchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sMode == "user")
            {
                sMode = "company";
                modeSwitchBtn.Content = "CodeOne";
                modeSwitchBtn.Background = Brushes.DarkRed;
                LoadDataGrid(sMode);
                //bool check for IsSuper to enable password editing
            }
            else if (sMode == "company")
            {
                sMode = "user";
                modeSwitchBtn.Content = "User";
                modeSwitchBtn.Background = Brushes.SteelBlue;
                LoadDataGrid(sMode);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cU.User = sUsername;
            userDecBlock.DataContext = cU;
            LoadDataGrid(sMode);
        }

        public void LoadDataGrid(string sMode)
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {

                if (sMode == "user")
                {
                    var selectQuery =
                        from a in dc.GetTable<PMUserSite>()
                        select a;
                    dataGrid.DataContext = selectQuery;
                }
                else if (sMode == "company")
                {
                    var selectQuery =
                        from a in dc.GetTable<PMCompanySite>()
                        select a;
                    dataGrid.DataContext = selectQuery;
                }
            }
        }
            
    }

        //public Visibility ShowButton
        //{
        //    get { return (OtherProperty ? Visibility.Collapsed : Visibility.Visible); }
        //}
        //<Button Visible = "{Binding Path=ShowButton}" />
    
}
