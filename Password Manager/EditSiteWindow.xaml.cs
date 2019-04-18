using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for EditSiteWindow.xaml
    /// </summary>
    public partial class EditSiteWindow : Window
    {
        string preEditId;
        string preEditPass;
        string preEditNotes;
        string preEditUrl;
        string postEditId;
        string postEditPass;
        string postEditUrl;
        string postEditNotes;
        public bool bModeIsCompany = MainWindow.bModeisCompany;

        public EditSiteWindow()
        {
            InitializeComponent();
            EditSitePracticeBox.IsEnabled = false;
            InitComboBox();
        }

        public void InitComboBox()
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                try
                {
                    if (!bModeIsCompany)
                    {
                        var query = from s in dc.PMUserSites
                                    where s.userName == MainWindow.sUsername
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        EditSiteNameBox.ItemsSource = distinct;
                    }
                    else if (bModeIsCompany)
                    {
                        var query = from s in dc.PMCompanySites
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        EditSiteNameBox.ItemsSource = distinct;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditSiteNameBox_DropDownClosed(object sender, EventArgs e)
        {
            var sn = EditSiteNameBox.Text;
            IEnumerable<int> query = null;
            EditSiteNotesBox.Clear();
            EditSitePassBox.Clear();
            EditSiteUrlBox.Clear();
            EditSiteIdBox.Clear();
            try
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                    if (bModeIsCompany)
                    {
                        query = from o in dc.PMCompanySites.AsEnumerable() where o.siteName == sn select o.practice;
                    }
                    else
                    {
                        query = from o in dc.PMUserSites.AsEnumerable() where o.siteName == sn select o.practice;
                    }
                    if (query.Count() > 1)
                    {
                        EditSitePracticeBox.IsEnabled = true;
                        EditSitePracticeBox.Items.Clear();
                        foreach (var p in query)
                        {
                            EditSitePracticeBox.Items.Add(p);
                        }
                        EditSitePracticeBox.Text = "Choose..";
                    }
                    else if (bModeIsCompany)
                    {
                        var singlequery = from o in dc.PMCompanySites where o.siteName == sn select o;
                        PMCompanySite pmcobj = new PMCompanySite();
                        pmcobj = singlequery.First();
                        LoadCompanySite(pmcobj);
                        EditSitePracticeBox.Text = "0";
                        EditSitePracticeBox.IsEnabled = false;
                    }
                    else
                    {
                        var singlequery = from o in dc.PMUserSites where o.siteName == sn select o;
                        PMUserSite pmuobj = new PMUserSite();
                        pmuobj = singlequery.First();
                        LoadUserSite(pmuobj);
                        EditSitePracticeBox.Text = "0";
                        EditSitePracticeBox.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditSitePracticeBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var prac = Int32.Parse(EditSitePracticeBox.Text);
                var sn = EditSiteNameBox.Text;
                if (!string.IsNullOrEmpty(sn) && (!string.IsNullOrEmpty(prac.ToString())))
                {
                    using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                    {
                        if (!bModeIsCompany)
                        {
                            var query = from o in dc.PMUserSites where o.siteName == sn && o.practice == prac select o;
                            PMUserSite pmuobj = new PMUserSite();
                            pmuobj = query.First();
                            LoadUserSite(pmuobj);
                        }
                        else if (bModeIsCompany)
                        {
                            var query = from o in dc.PMCompanySites where o.siteName == sn && o.practice == prac select o;
                            PMCompanySite pmcobj = new PMCompanySite();
                            pmcobj = query.First();
                            LoadCompanySite(pmcobj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadUserSite(PMUserSite obj)
        {
            EditSiteIdBox.Text = obj.siteId;
            preEditId = obj.siteId;
            EditSiteNotesBox.Text = obj.notes;
            preEditNotes = obj.notes;
            EditSitePassBox.Text = obj.sitePass;
            preEditPass = obj.sitePass;
            EditSiteUrlBox.Text = obj.siteUrl;
            preEditUrl = obj.siteUrl;
        }

        public void LoadCompanySite(PMCompanySite obj)
        {
            EditSiteIdBox.Text = obj.siteId;
            preEditId = obj.siteId;
            EditSiteNotesBox.Text = obj.notes;
            preEditNotes = obj.notes;
            EditSitePassBox.Text = obj.sitePass;
            preEditPass = obj.sitePass;
            EditSiteUrlBox.Text = obj.siteUrl;
            preEditUrl = obj.siteUrl;
        }

        public bool EditConfirmCheck()
        {
            postEditId = EditSiteIdBox.Text;
            postEditPass = EditSitePassBox.Text;
            postEditUrl = EditSiteUrlBox.Text;
            postEditNotes = EditSiteNotesBox.Text;
            string confirmText = "Are you sure you want to update the info to:\n " +
                "Login ID: " + preEditId + "  to  " + postEditId + "\n Login Password: " + preEditPass +
                "  to  " + postEditPass + "\n Website: " + postEditUrl + "\n Note: " + postEditNotes;

            var result = MessageBox.Show(confirmText, "Confirm Website Edit", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else  
            {
                return false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            var prac = Int32.Parse(EditSitePracticeBox.Text);
            var sn = EditSiteNameBox.Text;
            
            if (EditConfirmCheck())
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                        
                    if (!MainWindow.bModeisCompany)
                    {
                        PMUserSite pmu = dc.PMUserSites.Single(p => p.siteName == sn && p.practice == prac);
                        pmu.siteId = postEditId;
                        pmu.sitePass = postEditPass;
                        pmu.siteUrl = postEditUrl;
                        pmu.notes = postEditNotes;
                        try
                        {
                            dc.SubmitChanges();
                            MessageBox.Show("Successfully Updated");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Update Failed: \n\n"+ex.Message);
                        }
                            
                            
                            
                    }
                    else if (MainWindow.bModeisCompany)
                    {
                        PMCompanySite pmc = dc.PMCompanySites.Single(p => p.siteName == sn && p.practice == prac);
                        pmc.lastChanged = dt;
                        pmc.siteId = postEditId;
                        pmc.sitePass = postEditPass;
                        pmc.siteUrl = postEditUrl;
                        pmc.notes = postEditNotes;
                        pmc.lastChangedBy = MainWindow.sUsername;
                        try
                        {
                            dc.SubmitChanges();
                            MessageBox.Show("Successfully Updated");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Update Failed: \n\n" + ex.Message);
                        }
                    }
                        
                }
            }
            
        }
    }
}
