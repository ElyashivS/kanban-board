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

        public void Register(string email, string password)
        {
            email = email.Trim().ToLower();
            if (Users.ContainsKey(email))
                throw new ArgumentException("email is already in the system");
            
            Users.Add(email, new User(email, password));
            
                
        }
        public User Login(string email, string password)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
                throw new Exception("email does not exsist");
            Users[email].Login(email, password);

            return Users[email];
        }
        public void Logout(string email)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            Users[email].Logout();
            
        }
        public void ValidateUserLoggin(string email)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            else if (!Users[email].ValidateUserLoggin())
                throw new Exception("user is logged off");

            
        }

       
    }
}
