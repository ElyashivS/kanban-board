using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class SetNameVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private string setName;

        public string SetName
        {
            get => setName;
            set
            {
                setName = value;
                RaisePropertyChanged("SetName");
            }
        }
    }
}
