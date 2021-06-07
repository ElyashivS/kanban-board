using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class LoginVM
    {
        Service service = new Service();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }

        public void Login()
        {
            Response response = service.Login(Username, Password);
            if (response.ErrorOccured == true)
                Error = response.ErrorMessage;
            else
                Error = "Login succeeded";
        }
    }
}
