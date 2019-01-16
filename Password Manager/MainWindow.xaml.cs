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
using System.Data.SqlClient;
using System.Drawing;
using System.Deployment.Application;


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
        public static bool bWasCompanyMode = false;
        public static bool bPasswordHidden = true;
        

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
                bWasCompanyMode = true;
                InitComboBox(sMode);
                if (SQLAccess.IsSuperCheck(sUsername))
                {
                    EditMenu.IsEnabled = true;
                }
                else { EditMenu.IsEnabled = false; }
            }
            else if (sMode == "company")
            {
                sMode = "user";
                modeSwitchBtn.Content = "User";
                modeSwitchBtn.Background = Brushes.SteelBlue;
                InitComboBox(sMode);
                EditMenu.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cU.User = sUsername;
            userDecBlock.DataContext = cU;
            InitComboBox(sMode);
            practiceBox.IsEnabled = false;
            if (bPasswordHidden) { PassTextBox.Foreground = Brushes.White; }

        }

        public void InitComboBox(string sMode)
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                try
                {
                    if (sMode == "user")
                    {
                        var query = from s in dc.PMUserSites
                                    where s.userName == sUsername
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        siteNameBox.ItemsSource = distinct;

                    }
                    else if (sMode == "company")
                    {
                        var query = from s in dc.PMCompanySites
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        siteNameBox.ItemsSource = distinct;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bWasCompanyMode)
            {
                string msg = "Please remember to update any company wide login info that may have changed. Exit program?";
                MessageBoxResult result = MessageBox.Show(msg, "Update Reminder", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #region Copy buttons
        private void IdCopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(IdTextBox.Text);
        }

        private void PassCopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PassTextBox.Text);
        }
        #endregion
        #region Version display
        public Version AssemblyVersion
        {
            get
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
        }

        private void VersionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Version: " + AssemblyVersion.Major + "." + AssemblyVersion.Minor + "." + AssemblyVersion.Build + "." + AssemblyVersion.Revision);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        #endregion

        private void AddASiteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Show();
        }

        public void LoadPracticeBoxFromList(IEnumerable<int> list)
        {
            practiceBox.Items.Clear();
            foreach (var p in list)
            {
                practiceBox.Items.Add(p);
            }
        }

        

        public void MultiplePracticeCheck(string sn)
        {
            
            try
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                    var query = from o in dc.PMUserSites.AsEnumerable() where o.siteName == sn select o.practice;
                    if (query.Count() > 1)
                    {
                        practiceBox.IsEnabled = true;
                        LoadPracticeBoxFromList(query);
                    }
                    else
                    {
                        var singlequery = from o in dc.PMUserSites where o.siteName == sn select o;
                        PMUserSite pmuobj = new PMUserSite();
                        pmuobj = singlequery.First();
                        LoadFormFromObj(pmuobj);
                        practiceBox.Text = "0";
                        practiceBox.IsEnabled = false;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadFormFromObj(PMUserSite obj)
        {
            IdTextBox.Text = obj.siteId;
            PassTextBox.Text = obj.sitePass;
            NoteBox.Text = obj.notes;
            UrlBox.Text = obj.siteUrl;
        }


        private void SiteNameBox_DropDownClosed(object sender, EventArgs e)
        {
            var sn = siteNameBox.Text;
            MultiplePracticeCheck(sn);
        }

        private void ShowPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bPasswordHidden)
            {
                bPasswordHidden = false;
                PassTextBox.Foreground = Brushes.Black;
                ShowPasswordBtn.Content = FindResource("show");
            }
            else 
            {
                bPasswordHidden = true;
                PassTextBox.Foreground = Brushes.White;
                ShowPasswordBtn.Content = FindResource("hide");
            }
            

        }

        private void PracticeBox_DropDownClosed(object sender, EventArgs e)
        {
            var prac = Int32.Parse(practiceBox.Text);
            var sn = siteNameBox.Text;
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                var query = from o in dc.PMUserSites where o.siteName == sn && o.practice == prac select o;
                PMUserSite pmuobj = new PMUserSite();
                pmuobj = query.First();
                LoadFormFromObj(pmuobj);
            }
        }

        private void EditASiteMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    

}
