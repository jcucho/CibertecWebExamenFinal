using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> PagedList(int startRow, int endRow);
        int Count();
    }
}
