using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repositories.Chinook
{
    public interface IInvoiceLineRepository : IRepository<InvoiceLine>
    {
        IEnumerable<InvoiceLine> PagedList(int startRow, int endRow);
        int Count();
    }
}
