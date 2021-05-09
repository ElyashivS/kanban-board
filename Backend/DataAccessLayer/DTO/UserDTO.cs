using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    internal class UserDTO : DTO
    {
        private string _email;
        private string _password;

        public const string UserNameColumnName = "User";
        public const string PasswordColumnName = "Pass";

        public UserDTO(string email, string password) : base(new UserDalController())
        {
            _email = email;
            _password = password;
        }

        public string User { get => _email; set { _email = value; _controller.Update(_email, UserNameColumnName, value); } }
        public string Pass { get => _password; set { _password = value; _controller.Update(_email, PasswordColumnName, value); } }




    }
}
