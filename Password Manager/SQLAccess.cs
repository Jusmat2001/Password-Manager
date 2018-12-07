using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Password_Manager
{
    public static class SQLAccess
    {
        
        public static string ConnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static void AddUser(string susername, string spass)
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                PMUser nUser = new PMUser();
                nUser.DateRegistered = DateTime.Now;
                nUser.PMUsername = susername;
                nUser.PMPassword = spass;

                dc.PMUsers.InsertOnSubmit(nUser);

                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    throw;
                }
            }
        }

        public static bool LoginCheck(string sUser, string sPass)
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                var q = from p in dc.PMUsers
                    where p.PMUsername == sUser
                    && p.PMPassword == sPass
                    select p;
                

                if (q.Any())
                {
                    return true;
                }
                return false;
            }
        }

        public static bool IsSuperCheck(string sUser)
        {
            using (L2SAccessDataContext dc = new L2SAccessDataContext(SQLAccess.ConnVal("C1user")))
            {
                var q = from p in dc.PMUsers
                    where p.PMUsername == sUser
                    && p.CanEditCompany == true
                    select p;
                if (q.Any())
                {
                    return true;
                }
                return false;
            }
        }

        
    }
}
