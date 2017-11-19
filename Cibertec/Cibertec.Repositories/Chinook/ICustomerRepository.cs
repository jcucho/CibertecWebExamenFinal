using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> PagedList(int startRow, int endRow);
        int Count();
    }

}
