using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IInvoiceRepository: IRepository<Invoice>
    {
        IEnumerable<Invoice> PagedList(int startRow, int endRow);
        int Count();
    }
}
