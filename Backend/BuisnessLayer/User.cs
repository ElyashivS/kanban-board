using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class User
    {
        public string email;
        private string password;
        private bool isLoggedIn;

        public User(string email, string password)
        { 
            if (!IsValidEmail(email))
                throw new Exception("email is not valid");
            this.email = email;

            PasswordVerifier(password);
            this.password = password;


            isLoggedIn = false;
        }

        public void Login(string email, string password)
        {
            if (this.email == email && this.password == password)
            {
                isLoggedIn = true;

            }
            else
                throw new Exception("email or password does not match");
        }

        public void Logout()
        {
            isLoggedIn = false;
        }

        //copied from https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                   @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                   + "@"
                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        

        public bool ValidateUserLoggin()
        {
            return isLoggedIn;

        }

        private void PasswordVerifier(string password)
        {
           
            
                if (password == null)
                    throw new NullReferenceException();
                if (password.Length > 20 ||password.Length < 4)
                    throw new Exception("password must be in length of 4 to 20 characters");
                bool isUpper = false;
                bool isLower = false;
                bool isDigit = false;
                foreach (char c in password)
                {
                    if (char.IsLower(c))
                        isLower = true;
                    if (char.IsUpper(c))
                        isUpper = true;
                    if (char.IsDigit(c))
                        isDigit = true;
                    if (c==' ')
                        throw new Exception("space is not valid in password");



                }
                if (!isUpper || !isLower || !isDigit)
                    throw new Exception(" must include at least one uppercase letter, one small character and a number.");

                this.password = password;

            
        }
        
        
    }
}
