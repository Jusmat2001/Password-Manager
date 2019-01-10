using System.Collections.Generic;
using System.Linq;

namespace Password_Manager
{
    partial class PMUser
    {
        
    }

    public partial class PMUserSite
    {
        public PMUserSite (string siteId, string sitePass, string notes, string siteUrl)
        {
            _siteId = siteId;
            _sitePass = sitePass;
            _notes = notes;
            _siteUrl = siteUrl;
        }

        public void LoadPMUserSite(PMUserSite obj)
        {
            

        }
    }

}