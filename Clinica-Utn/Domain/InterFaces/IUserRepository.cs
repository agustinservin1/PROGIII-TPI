using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IUserRepository
    {
        User? Authenticate(string email, string password);
        User? ValidateEmail(string email);
    }
}
