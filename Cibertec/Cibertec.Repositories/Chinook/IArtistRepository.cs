using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IArtistRepository : IRepository<Artist>
    {
        IEnumerable<Artist> PagedList(int startRow, int endRow);
        int Count();
    }
}
