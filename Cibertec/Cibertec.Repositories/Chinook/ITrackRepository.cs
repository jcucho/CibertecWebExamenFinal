using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface ITrackRepository : IRepository<Track>
    {
        IEnumerable<Track> PagedList(int startRow, int endRow);
        int Count();
    }
}
