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

        public const string usernameColumnName = "Email";
        public const string passwordColumnName = "Password";

        public UserDTO(string email, string password) : base(new UserDalController())
        {
            _email = email;
            _password = password;
        }

        public string Email { get => _email; set { _email = value; } }
        public string Password { get => _password; set { _password = value; } }




    }
}
