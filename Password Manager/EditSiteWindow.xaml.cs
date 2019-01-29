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

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for EditSiteWindow.xaml
    /// </summary>
    public partial class EditSiteWindow : Window
    {
        public EditSiteWindow()
        {
            InitializeComponent();
            
            InitComboBox();
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
                        EditSiteNameBox.ItemsSource = distinct;
                    }
                    else if (MainWindow.bModeisCompany)
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

        }
    }
}
