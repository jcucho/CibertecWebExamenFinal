using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IGenreRepository : IRepository<Genre>
    {
        IEnumerable<Genre> PagedList(int startRow, int endRow);
        int Count();
    }
}
