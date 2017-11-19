using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IUserRepository : IRepository<User>
    {
        User ValidaterUser(string email, string password);
    }
}
