using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class SetLimitToColumnVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private string setLimit;

        public string SetLimit
        {
            get => setLimit;
            set
            {
                setLimit = value;
                RaisePropertyChanged("SetLimit");
            }
        }
    }
}
