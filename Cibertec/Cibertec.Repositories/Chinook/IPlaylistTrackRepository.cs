using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IPlaylistTrackRepository : IRepository<PlaylistTrack>
    {
        IEnumerable<PlaylistTrack> PagedList(int startRow, int endRow);
        int Count();
    }
}
