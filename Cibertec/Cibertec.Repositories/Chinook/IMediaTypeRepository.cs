using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IMediaTypeRepository : IRepository<MediaType>
    {
        IEnumerable<MediaType> PagedList(int startRow, int endRow);
        int Count();
    }
}
