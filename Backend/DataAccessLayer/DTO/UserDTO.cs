using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    class UserDTO : DTO
    {
        public const string UserNameColumnName = "User";


        private string _email;
        private string _password;
        public string User { get => _email; }

        public UserDTO(string email, string password) : base(new UserDalController())
        {
            _email = email;
            _password = password;
        }
    }
}
