using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class UserController
    {
        Dictionary<string, User> Users;

        public UserController()
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
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            Users[email].Logout();
        }
        public bool isLoggedin(string email)
        {
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            return Users[email].IsLoggedIn;
        }

        internal void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
