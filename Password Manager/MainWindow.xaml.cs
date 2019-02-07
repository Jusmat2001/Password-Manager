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
using System.Deployment;
using System.Deployment.Application;
using System.Diagnostics;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static bool bModeisCompany = false;
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
            IdTextBox.Clear();
            PassTextBox.Clear();
            UrlBox.Document.Blocks.Clear();

            if (!bModeisCompany)
            {
                bModeisCompany= true;
                modeSwitchBtn.Content = "CodeOne";
                modeSwitchBtn.Background = Brushes.DarkRed;
                bWasCompanyMode = true;
                InitComboBox();
                if (SQLAccess.IsSuperCheck(sUsername))
                {
                    EditMenu.IsEnabled = true;
                }
                else { EditMenu.IsEnabled = false; }
            }
            else if (bModeisCompany)
            {
                bModeisCompany = false ;
                modeSwitchBtn.Content = "User";
                modeSwitchBtn.Background = Brushes.SteelBlue;
                InitComboBox();
                EditMenu.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cU.User = sUsername;
            userDecBlock.DataContext = cU;
            InitComboBox();
            practiceBox.IsEnabled = false;
            if (bPasswordHidden) { PassTextBox.Foreground = Brushes.White; }

        }

        public void InitComboBox()
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                try
                {
                    if (!bModeisCompany)
                    {
                        var query = from s in dc.PMUserSites
                                    where s.userName == sUsername
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        siteNameBox.ItemsSource = distinct;
                    }
                    else if (bModeisCompany)
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

        private void VersionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Version myVersion;

                //if (ApplicationDeployment.IsNetworkDeployed)
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                var msg = string.Concat("ClickOnce published Version: "+myVersion);
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public void LoadUserSite(PMUserSite obj)
        {
            IdTextBox.Text = obj.siteId;
            PassTextBox.Text = obj.sitePass;
            NoteBox.Text = obj.notes;
            AddHyperLink(obj.siteUrl);
        }

        public void LoadCompanySite(PMCompanySite obj)
        {
            IdTextBox.Text = obj.siteId;
            PassTextBox.Text = obj.sitePass;
            NoteBox.Text = obj.notes;
            AddHyperLink(obj.siteUrl);
            
        }

        private void AddHyperLink(string site)
        {
            try
            {
                FlowDocument fd = new FlowDocument();
                Paragraph para = new Paragraph();
                Hyperlink link = new Hyperlink();
                link.IsEnabled = true;
                link.Inlines.Add(site);
                link.NavigateUri = new Uri(site);
                link.RequestNavigate += (sender, args) => Process.Start(args.Uri.ToString());

                para.Inlines.Add(link);
                fd.Blocks.Add(para);
                UrlBox.Document = fd;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hyperlink unavailable. Please make sure the web address was copied directly from the address bar of the working web page you would like to link. Please see Justin for further assistance.");
            }
            
            
        }

        

        private void SiteNameBox_DropDownClosed(object sender, EventArgs e)
        {
            var sn = siteNameBox.Text;
            IEnumerable<int> query = null;
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                if (!string.IsNullOrEmpty(sn))
                {
                    try
                    {
                        if (bModeisCompany)
                        {
                            query = from o in dc.PMCompanySites.AsEnumerable() where o.siteName == sn select o.practice;
                        }
                        else
                        {
                            query = from o in dc.PMUserSites.AsEnumerable() where o.siteName == sn select o.practice;
                        }
                        if (query.Count() > 1) { practiceBox.IsEnabled = true; LoadPracticeBoxFromList(query); }
                        else
                        {
                            var singlequery = from o in dc.PMUserSites where o.siteName == sn select o;
                            PMUserSite pmuobj = new PMUserSite();
                            pmuobj = singlequery.First();
                            LoadUserSite(pmuobj);
                            practiceBox.Text = "0";
                            practiceBox.IsEnabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                
            }
            
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
            if (!string.IsNullOrEmpty(sn) && (!string.IsNullOrEmpty(prac.ToString())))
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                    if (!bModeisCompany)
                    {
                        var query = from o in dc.PMUserSites where o.siteName == sn && o.practice == prac select o;
                        PMUserSite pmuobj = new PMUserSite();
                        pmuobj = query.First();
                        LoadUserSite(pmuobj);
                    }
                    else if (bModeisCompany)
                    {
                        var query = from o in dc.PMCompanySites where o.siteName == sn && o.practice == prac select o;
                        PMCompanySite pmcobj = new PMCompanySite();
                        pmcobj = query.First();
                        LoadCompanySite(pmcobj);
                    }
                }
            }
        }

        private void EditASiteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditSiteWindow ew = new EditSiteWindow();
            ew.Show();
        }

        private void DeleteASiteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Will be added in a future version. Please email Justin if you want a site removed. ");
        }
    }
    

}
