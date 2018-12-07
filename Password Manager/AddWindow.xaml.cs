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

        

        public AddWindow()
        {
            InitializeComponent();
            LoadPracticeBox();
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
                    pmu.siteId = AddUsernameBox.Text;
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

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddSiteNameBox.Text == string.Empty || AddWebAddressBox.Text == string.Empty || AddUsernameBox.Text == string.Empty || AddPracticeBox.SelectedValue == null)
            {
                MessageBox.Show("Please complete the required fields" );
                return;
            }
            
            if (AddUserSite())
            {
                MessageBox.Show("Site added");
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
        }

        private void AddNotesBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= AddNotesBox_GotFocus;
        }
        #endregion
    }
}
