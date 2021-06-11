using Frontend.Model;
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
        public BackendController backendController { get; private set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
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


        internal UserModel Login()
        {
            //Error = "";
            //Response response = service.Login(Email, Password);
            //if (response.ErrorOccured == true)
            //{
            //    Error = response.ErrorMessage;
            //    return null;
            //}
            //else
            //{
            //    try
            //    {
            //        return backendController.Login(Email, Password);
            //    }
            //    catch (Exception e)
            //    {
            //        Error = e.Message;
            //        return null;
            //    }
            //} 
            Error = "";
            try
            {
                return backendController.Login(Email, Password);
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null;
            }
        }

        internal void Register()
        {
            //Response response = service.Register(Email, Password);
            //if (response.ErrorOccured == true)
            //    Error = response.ErrorMessage;
            //else
            //{
            //    Error = "Register succeeded";
            //}
            Error = "";
            try
            {
                backendController.Register(Email, Password);
                Error = "Registered successfully";
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
        public LoginVM()
        {
            this.backendController = new BackendController();
        }
    }
}
