using System.Collections.Generic;
using System.Linq;

namespace Password_Manager
{
    partial class PMUser
    {
        
    }

    public partial class PMUserSite
    {
        public PMUserSite (string siteName)
        {
            
        }

        public void LoadPMUserSite(PMUserSite obj)
        {
            siteId = obj.siteId;
            sitePass = obj.sitePass;
            notes = obj.notes;
            siteUrl = obj.siteUrl;

        }
    }

}