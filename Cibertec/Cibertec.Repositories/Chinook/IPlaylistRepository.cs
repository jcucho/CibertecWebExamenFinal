using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        IEnumerable<Playlist> PagedList(int startRow, int endRow);
        int Count();
    }
}
