using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository

    {
       private readonly ApplicationContext _context;
       public UserRepository(ApplicationContext context) 
       {
            _context = context; 
       }

        public User? Authenticate(string email, string password)
        {
            User? userToAuthenticate = _context.Set<User>().FirstOrDefault(u => u.Email == email && u.Password == password);
            return userToAuthenticate;
        }
        public User? ValidateEmail (string email) {
            User? validateUserEmail = _context.Set<User>().FirstOrDefault(u=>u.Email == email); 
            return validateUserEmail;
    }
}
}
