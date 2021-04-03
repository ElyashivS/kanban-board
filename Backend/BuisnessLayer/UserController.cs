using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    class UserController
    {
        Dictionary<string, User> Users;

        internal UserController()
        {
            Users = new Dictionary<string, User>();
        }

        public User Register(string email, string password)
        {
            if (!Users.ContainsKey(email))
            {
                Users.Add(email, new User(email, password));
                return Users[email];
            }
            else
                throw new Exception("email is already in the system");
        }
        public User Login(string email, string password)
        {
            if (!Users.ContainsKey(email))
                throw new Exception("email does not exsist");
            else
                Users[email].Login(email, password);

            return Users[email];

        }
        public void Logout(string email)
        {
            Users[email].Logout();
        }
        public bool isLoggedin(string email)
        {
            return Users[email].IsLoggedIn;
        }
    }
}
