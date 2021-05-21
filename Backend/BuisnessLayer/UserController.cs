using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
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
        UserDalController UsersTable = new UserDalController();

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
            UserDTO toInsert = new UserDTO(email, password);
            UsersTable.Insert(toInsert);
                
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
        public bool ValidateUserLoggin(string email)
        {
            email = email.Trim().ToLower();
            if (!Users.ContainsKey(email))
            {
                throw new Exception("email does not exsist");
            }
            else if (!Users[email].ValidateUserLoggin())
                throw new Exception("user is logged off");

            return true;

            
        }
        public void DeleteData()
        {
            try
            {
                UsersTable.DeleteUserTable();
            }
            catch
            {
                throw new Exception("could not delete all the users");
            }
        }
        public void LoadData()
        {
            List<DTO> toload=UsersTable.Select();
            foreach (UserDTO user in toload)
            {
                
                RegisterForLoad(user.Email, user.Password);
                
            }
        }
        public List<User> UsersToList()
        {
            return Users.Values.ToList();
        }
        public List<string> BringAllUsersEmail()
        {
            List<string> toreturn = new List<string>();
            List<User> a = UsersToList();
            foreach (User b in a)
            {
                toreturn.Add(b.email);
            }
            return toreturn;
        }
        private void RegisterForLoad(string email, string password)
        {
            email = email.Trim().ToLower();
            if (Users.ContainsKey(email))
                throw new ArgumentException("email is already in the system");

            Users.Add(email, new User(email, password));
        }
    }
    
}
