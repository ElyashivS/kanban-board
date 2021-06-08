using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    internal class LoginVM : Notifiable
    {
        Service service = new Service();

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string error;
        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged("Error");
            }
        }


        internal void Login()
        {
            Response response = service.Login(Username, Password);
            if (response.ErrorOccured == true)
                Error = response.ErrorMessage;
            else
            {
                Error = "Login succeeded";
            }
        }

        internal void Register()
        {
            Response response = service.Register(Username, Password);
            if (response.ErrorOccured == true)
                Error = response.ErrorMessage;
            else
            {
                Error = "Register succeeded";
            }
        }
    }
}
