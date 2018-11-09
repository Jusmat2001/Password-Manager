using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class cUser : INotifyPropertyChanged
    {
        private string _user;
        private string _isSuper;

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        public string IsSuper
        {
            get { return _isSuper; }
            set
            {
                _isSuper = value;
                RaisePropertyChanged("IsSuper");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
