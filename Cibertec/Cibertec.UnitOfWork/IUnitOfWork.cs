﻿using Cibertec.Repositories.Chinook;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAlbumRepository Albums { get; }
        IArtistRepository Artists { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IGenreRepository Genres { get; }
        IInvoiceRepository Invoices { get; }
        IInvoiceLineRepository InvoiceLines { get; }        
        IMediaTypeRepository MediaTypes { get; }
        IPlaylistRepository Playlists { get; }
        IPlaylistTrackRepository PlaylistTracks { get; }
        ITrackRepository Tracks { get; }
        IUserRepository Users { get; }
    }
}
