using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class SQLAccess
    {
        private string sQuery = "";
        public static DataContext db = new DataContext(SQLAccess.ConnVal("C1user"));

        public static string ConnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public void AddNewUser(string susername, string spass, DateTime dtregDate)
        {
            var onuser = new UserObj();
            onuser.PMUsername = susername;
            onuser.PMPassword = spass;
            onuser.DateRegistered = dtregDate;
            //db.insert a new user
        }

    }
}
