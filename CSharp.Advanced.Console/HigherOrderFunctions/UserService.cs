using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.HigherOrderFunctions
{
    public class UserService
    {

        public OneOf<User, ValidationError> CreateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ValidationError("Email is required");
            }

            if (password.Length < 8)
            {
                return new ValidationError("Password too short");
            }

            return new User(email, password);
        }
    }

    public class User
    {
        private string email;
        private string password;

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    

    public class ValidationError
    {
        private string v;

        public ValidationError(string v)
        {
            this.v = v;
        }
    }
}
