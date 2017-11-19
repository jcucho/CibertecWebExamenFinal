using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IAlbumRepository : IRepository<Album>
    {
        IEnumerable<Album> PagedList(int startRow, int endRow);
        int Count();
    }
}
