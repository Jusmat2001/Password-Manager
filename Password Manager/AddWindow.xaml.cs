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

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public bool PasswordBoxGotFocus = false;

        

        public AddWindow()
        {
            InitializeComponent();
            LoadPracticeBox();
            InitComboBox();
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadPracticeBox()
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                Practice_Table pt = new Practice_Table();
                var query = from s in dc.Practice_Tables
                            select s.PracticeID;
                AddPracticeBox.ItemsSource = query;
            }
        }

        public bool AddUserSite()
        {
            try
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                    PMUserSite pmu = new PMUserSite();
                    pmu.practice = Int32.Parse(AddPracticeBox.SelectedValue.ToString());
                    pmu.siteName = AddSiteNameBox.Text;
                    pmu.siteUrl = AddWebAddressBox.Text;
                    pmu.siteId = AddLoginIdBox.Text;
                    pmu.sitePass = AddPasswordBox.Text;
                    pmu.userName = MainWindow.sUsername;
                    pmu.notes = AddNotesBox.Text;
                    dc.PMUserSites.InsertOnSubmit(pmu);
                    dc.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool AddCompanySite()
        {
            try
            {
                using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                {
                    PMCompanySite pmc = new PMCompanySite();
                    pmc.practice = Int32.Parse(AddPracticeBox.SelectedValue.ToString());
                    pmc.siteName = AddSiteNameBox.Text;
                    pmc.siteUrl = AddWebAddressBox.Text;
                    pmc.siteId = AddLoginIdBox.Text;
                    pmc.sitePass = AddPasswordBox.Text;
                    pmc.lastChanged = DateTime.Now;
                    pmc.notes = AddNotesBox.Text;
                    pmc.lastChangedBy = MainWindow.sUsername;
                    dc.PMCompanySites.InsertOnSubmit(pmc);
                    dc.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddSiteNameBox.Text == string.Empty || AddWebAddressBox.Text == string.Empty || AddLoginIdBox.Text == string.Empty || AddPracticeBox.SelectedValue == null)
            {
                MessageBox.Show("Please complete the required fields" );
                return;
            }
            if (AddNotesBox.Text == "Notes") { AddNotesBox.Text = null; }
            if (AddPasswordBox.Text == "Password" && PasswordBoxGotFocus == false ) { AddPasswordBox.Text = null; }
            
            if (MainWindow.bModeisCompany)
            {
                if (AddCompanySite())
                {
                    MessageBox.Show("Company site added");
                }
                else { MessageBox.Show("Site not added"); }
            }
            else if (!MainWindow.bModeisCompany)
            {
                if (AddUserSite())
                {
                    MessageBox.Show("User site added");
                }
                else { MessageBox.Show("Site not added"); }
            }
        }

        #region text clearers
        private void AddSiteNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddSiteNameBox_GotFocus;
        }

        private void AddPracticeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddPracticeBox_GotFocus;
        }

        private void AddWebAddressBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddWebAddressBox_GotFocus;
        }

        private void AddUsernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddUsernameBox_GotFocus;
        }

        private void AddPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddPasswordBox_GotFocus;
            PasswordBoxGotFocus = true;
        }

        private void AddNotesBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddNotesBox_GotFocus;
        }
        #endregion

        private void CbAddSite_DropDownClosed(object sender, EventArgs e)
        {
            var sn = cbAddSite.Text;
            IEnumerable<int> query = null;
            AddSiteNameBox.Clear();
            AddPasswordBox.Clear();
            AddLoginIdBox.Clear();
            AddNotesBox.Clear();
            AddWebAddressBox.Clear();
            
            try
            {
                if (!string.IsNullOrEmpty(sn))
                {
                    using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
                    {
                        if (MainWindow.bModeisCompany)
                        {
                            query = from o in dc.PMCompanySites.AsEnumerable() where o.siteName == sn select o.practice;
                        }
                        else
                        {
                            query = from o in dc.PMUserSites.AsEnumerable() where o.siteName == sn select o.practice;
                        }
                        //set prac box to Choose..
                        //fill site, name, notes boxes
                        //add site
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InitComboBox()
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                try
                {
                    if (!MainWindow.bModeisCompany)
                    {
                        var query = from s in dc.PMUserSites
                                    where s.userName == MainWindow.sUsername
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        cbAddSite.ItemsSource = distinct;
                    }
                    else if (MainWindow.bModeisCompany)
                    {
                        var query = from s in dc.PMCompanySites
                                    select s.siteName.AsEnumerable();
                        var distinct = query.Distinct().ToList();
                        cbAddSite.ItemsSource = distinct;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
