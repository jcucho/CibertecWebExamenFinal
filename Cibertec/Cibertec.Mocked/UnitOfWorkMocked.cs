using Ploeh.AutoFixture;
using System.Collections.Generic;
using Cibertec.Repositories.Chinook;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using Moq;
using System.Linq;

namespace Cibertec.Mocked
{
    public class UnitOfWorkMocked
    {
        private List<Customer> _customers;
        private List<Album> _albums;
        private List<Artist> _artists;
        private List<Employee> _employees;
        private List<Genre> _genres;
        private List<Invoice> _invoices;
        private List<InvoiceLine> _invoiceLines;
        private List<MediaType> _mediaTypes;
        private List<Playlist> _playlists;
        private List<PlaylistTrack> _playlistTracks;
        private List<Track> _tracks;

        public UnitOfWorkMocked()
        {
            _customers = Customers();
            _albums = Albums();
            _artists = Artist();
            _employees = Employee();
            _genres = Genre();
            _invoices = Invoice();
            _invoiceLines = InvoiceLine();
            _mediaTypes = MediaType();
            _playlists = Playlist();
            _playlistTracks = PlaylistTrack();
            _tracks = Track();
        }

        public IUnitOfWork GetInstance()
        {
            var mocked = new Mock<IUnitOfWork>();
            mocked.Setup(u => u.Customers).Returns(CustomerRepositoryMocked());
            mocked.Setup(u => u.Albums).Returns(AlbumRepositoryMocked());
            mocked.Setup(u => u.Artists).Returns(ArtistRepositoryMocked());
            mocked.Setup(u => u.Employees).Returns(EmployeeRepositoryMocked());
            mocked.Setup(u => u.Genres).Returns(GenreRepositoryMocked());
            mocked.Setup(u => u.Invoices).Returns(InvoiceRepositoryMocked());
            mocked.Setup(u => u.InvoiceLines).Returns(InvoiceLineRepositoryMocked());
            mocked.Setup(u => u.MediaTypes).Returns(MediaTypeRepositoryMocked());
            mocked.Setup(u => u.Playlists).Returns(PlaylistRepositoryMocked());
            mocked.Setup(u => u.PlaylistTracks).Returns(PlaylistTrackRepositoryMocked());
            mocked.Setup(u => u.Tracks).Returns(TrackRepositoryMocked());
            return mocked.Object;
        }

        private ITrackRepository TrackRepositoryMocked()
        {
            var trackMocked = new Mock<ITrackRepository>();
            trackMocked.Setup(c => c.GetList()).Returns(_tracks);
            trackMocked.Setup(c => c.Insert(It.IsAny<Track>())).Callback<Track>((c) => _tracks.Add(c)).Returns<Track>(c => c.TrackId);
            trackMocked.Setup(c => c.Delete(It.IsAny<Track>())).Callback<Track>((c) => _tracks.RemoveAll(cus => cus.TrackId == c.TrackId)).Returns(true);
            trackMocked.Setup(c => c.Update(It.IsAny<Track>())).Callback<Track>((c) => { _tracks.RemoveAll(cus => cus.TrackId == c.TrackId); _tracks.Add(c); }).Returns(true);
            trackMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _tracks.FirstOrDefault(cus => cus.TrackId == id));
            return trackMocked.Object;
        }

        private IPlaylistTrackRepository PlaylistTrackRepositoryMocked()
        {
            var playlistTrackMocked = new Mock<IPlaylistTrackRepository>();
            playlistTrackMocked.Setup(c => c.GetList()).Returns(_playlistTracks);
            playlistTrackMocked.Setup(c => c.Insert(It.IsAny<PlaylistTrack>())).Callback<PlaylistTrack>((c) => _playlistTracks.Add(c)).Returns<PlaylistTrack>(c => c.PlaylistId);
            playlistTrackMocked.Setup(c => c.Delete(It.IsAny<PlaylistTrack>())).Callback<PlaylistTrack>((c) => _playlistTracks.RemoveAll(cus => cus.PlaylistId == c.PlaylistId)).Returns(true);
            playlistTrackMocked.Setup(c => c.Update(It.IsAny<PlaylistTrack>())).Callback<PlaylistTrack>((c) => { _playlistTracks.RemoveAll(cus => cus.PlaylistId == c.PlaylistId); _playlistTracks.Add(c); }).Returns(true);
            playlistTrackMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _playlistTracks.FirstOrDefault(cus => cus.PlaylistId == id));
            return playlistTrackMocked.Object;
        }

        private IPlaylistRepository PlaylistRepositoryMocked()
        {
            var playlistMocked = new Mock<IPlaylistRepository>();
            playlistMocked.Setup(c => c.GetList()).Returns(_playlists);
            playlistMocked.Setup(c => c.Insert(It.IsAny<Playlist>())).Callback<Playlist>((c) => _playlists.Add(c)).Returns<Playlist>(c => c.PlaylistId);
            playlistMocked.Setup(c => c.Delete(It.IsAny<Playlist>())).Callback<Playlist>((c) => _playlists.RemoveAll(cus => cus.PlaylistId == c.PlaylistId)).Returns(true);
            playlistMocked.Setup(c => c.Update(It.IsAny<Playlist>())).Callback<Playlist>((c) => { _playlists.RemoveAll(cus => cus.PlaylistId == c.PlaylistId); _playlists.Add(c); }).Returns(true);
            playlistMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _playlists.FirstOrDefault(cus => cus.PlaylistId == id));
            return playlistMocked.Object;
        }


        private IMediaTypeRepository MediaTypeRepositoryMocked()
        {
            var mediaTypeMocked = new Mock<IMediaTypeRepository>();
            mediaTypeMocked.Setup(c => c.GetList()).Returns(_mediaTypes);
            mediaTypeMocked.Setup(c => c.Insert(It.IsAny<MediaType>())).Callback<MediaType>((c) => _mediaTypes.Add(c)).Returns<MediaType>(c => c.MediaTypeId);
            mediaTypeMocked.Setup(c => c.Delete(It.IsAny<MediaType>())).Callback<MediaType>((c) => _mediaTypes.RemoveAll(cus => cus.MediaTypeId == c.MediaTypeId)).Returns(true);
            mediaTypeMocked.Setup(c => c.Update(It.IsAny<MediaType>())).Callback<MediaType>((c) => { _mediaTypes.RemoveAll(cus => cus.MediaTypeId == c.MediaTypeId); _mediaTypes.Add(c); }).Returns(true);
            mediaTypeMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _mediaTypes.FirstOrDefault(cus => cus.MediaTypeId == id));
            return mediaTypeMocked.Object;
        }

        private IInvoiceLineRepository InvoiceLineRepositoryMocked()
        {
            var invoiceLineMocked = new Mock<IInvoiceLineRepository>();
            invoiceLineMocked.Setup(c => c.GetList()).Returns(_invoiceLines);
            invoiceLineMocked.Setup(c => c.Insert(It.IsAny<InvoiceLine>())).Callback<InvoiceLine>((c) => _invoiceLines.Add(c)).Returns<InvoiceLine>(c => c.InvoiceLineId);
            invoiceLineMocked.Setup(c => c.Delete(It.IsAny<InvoiceLine>())).Callback<InvoiceLine>((c) => _invoiceLines.RemoveAll(cus => cus.InvoiceLineId == c.InvoiceLineId)).Returns(true);
            invoiceLineMocked.Setup(c => c.Update(It.IsAny<InvoiceLine>())).Callback<InvoiceLine>((c) => { _invoiceLines.RemoveAll(cus => cus.InvoiceLineId == c.InvoiceLineId); _invoiceLines.Add(c); }).Returns(true);
            invoiceLineMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _invoiceLines.FirstOrDefault(cus => cus.InvoiceLineId == id));
            return invoiceLineMocked.Object;
        }

        private IInvoiceRepository InvoiceRepositoryMocked()
        {
            var invoiceMocked = new Mock<IInvoiceRepository>();
            invoiceMocked.Setup(c => c.GetList()).Returns(_invoices);
            invoiceMocked.Setup(c => c.Insert(It.IsAny<Invoice>())).Callback<Invoice>((c) => _invoices.Add(c)).Returns<Invoice>(c => c.InvoiceId);
            invoiceMocked.Setup(c => c.Delete(It.IsAny<Invoice>())).Callback<Invoice>((c) => _invoices.RemoveAll(cus => cus.InvoiceId == c.InvoiceId)).Returns(true);
            invoiceMocked.Setup(c => c.Update(It.IsAny<Invoice>())).Callback<Invoice>((c) => { _invoices.RemoveAll(cus => cus.InvoiceId == c.InvoiceId); _invoices.Add(c); }).Returns(true);
            invoiceMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _invoices.FirstOrDefault(cus => cus.InvoiceId == id));
            return invoiceMocked.Object;
        }

        private IGenreRepository GenreRepositoryMocked()
        {
            var genreMocked = new Mock<IGenreRepository>();
            genreMocked.Setup(c => c.GetList()).Returns(_genres);
            genreMocked.Setup(c => c.Insert(It.IsAny<Genre>())).Callback<Genre>((c) => _genres.Add(c)).Returns<Genre>(c => c.GenreId);
            genreMocked.Setup(c => c.Delete(It.IsAny<Genre>())).Callback<Genre>((c) => _genres.RemoveAll(cus => cus.GenreId == c.GenreId)).Returns(true);
            genreMocked.Setup(c => c.Update(It.IsAny<Genre>())).Callback<Genre>((c) => { _genres.RemoveAll(cus => cus.GenreId == c.GenreId); _genres.Add(c); }).Returns(true);
            genreMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _genres.FirstOrDefault(cus => cus.GenreId == id));
            return genreMocked.Object;
        }

        private IEmployeeRepository EmployeeRepositoryMocked()
        {
            var employeeMocked = new Mock<IEmployeeRepository>();
            employeeMocked.Setup(c => c.GetList()).Returns(_employees);
            employeeMocked.Setup(c => c.Insert(It.IsAny<Employee>())).Callback<Employee>((c) => _employees.Add(c)).Returns<Employee>(c => c.EmployeeId);
            employeeMocked.Setup(c => c.Delete(It.IsAny<Employee>())).Callback<Employee>((c) => _employees.RemoveAll(cus => cus.EmployeeId == c.EmployeeId)).Returns(true);
            employeeMocked.Setup(c => c.Update(It.IsAny<Employee>())).Callback<Employee>((c) => { _employees.RemoveAll(cus => cus.EmployeeId == c.EmployeeId); _employees.Add(c); }).Returns(true);
            employeeMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _employees.FirstOrDefault(cus => cus.EmployeeId == id));
            return employeeMocked.Object;
        }

        private ICustomerRepository CustomerRepositoryMocked()
        {
            var customerMocked = new Mock<ICustomerRepository>();
            customerMocked.Setup(c => c.GetList()).Returns(_customers);
            customerMocked.Setup(c => c.Insert(It.IsAny<Customer>())).Callback<Customer>((c) => _customers.Add(c)).Returns<Customer>(c => c.CustomerId);
            customerMocked.Setup(c => c.Delete(It.IsAny<Customer>())).Callback<Customer>((c) => _customers.RemoveAll(cus => cus.CustomerId == c.CustomerId)).Returns(true);
            customerMocked.Setup(c => c.Update(It.IsAny<Customer>())).Callback<Customer>((c) => { _customers.RemoveAll(cus => cus.CustomerId == c.CustomerId); _customers.Add(c); }).Returns(true);
            customerMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _customers.FirstOrDefault(cus => cus.CustomerId == id));
            return customerMocked.Object;
        }

        private IAlbumRepository AlbumRepositoryMocked()
        {
            var albumMocked = new Mock<IAlbumRepository>();
            albumMocked.Setup(c => c.GetList()).Returns(_albums);
            albumMocked.Setup(c => c.Insert(It.IsAny<Album>())).Callback<Album>((c) => _albums.Add(c)).Returns<Album>(c => c.AlbumId);
            albumMocked.Setup(c => c.Delete(It.IsAny<Album>())).Callback<Album>((c) => _albums.RemoveAll(cus => cus.AlbumId == c.AlbumId)).Returns(true);
            albumMocked.Setup(c => c.Update(It.IsAny<Album>())).Callback<Album>((c) => { _albums.RemoveAll(cus => cus.AlbumId == c.AlbumId); _albums.Add(c); }).Returns(true);
            albumMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _albums.FirstOrDefault(cus => cus.AlbumId == id));
            return albumMocked.Object;
        }

        private IArtistRepository ArtistRepositoryMocked()
        {
            var artistMocked = new Mock<IArtistRepository>();
            artistMocked.Setup(c => c.GetList()).Returns(_artists);
            artistMocked.Setup(c => c.Insert(It.IsAny<Artist>())).Callback<Artist>((c) => _artists.Add(c)).Returns<Artist>(c => c.ArtistId);
            artistMocked.Setup(c => c.Delete(It.IsAny<Artist>())).Callback<Artist>((c) => _artists.RemoveAll(cus => cus.ArtistId == c.ArtistId)).Returns(true);
            artistMocked.Setup(c => c.Update(It.IsAny<Artist>())).Callback<Artist>((c) => { _artists.RemoveAll(cus => cus.ArtistId == c.ArtistId); _artists.Add(c); }).Returns(true);
            artistMocked.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => _artists.FirstOrDefault(cus => cus.ArtistId == id));
            return artistMocked.Object;
        }

        private List<Track> Track()
        {
            var fixture = new Fixture();
            var tracks = fixture.CreateMany<Track>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                tracks[i].TrackId = i + 1;
            }
            return tracks;
        }

        private List<PlaylistTrack> PlaylistTrack()
        {
            var fixture = new Fixture();
            var playlistTracks = fixture.CreateMany<PlaylistTrack>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                playlistTracks[i].PlaylistId = i + 1;
            }
            return playlistTracks;
        }

        private List<Playlist> Playlist()
        {
            var fixture = new Fixture();
            var playlists = fixture.CreateMany<Playlist>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                playlists[i].PlaylistId = i + 1;
            }
            return playlists;
        }

        private List<MediaType> MediaType()
        {
            var fixture = new Fixture();
            var mediaTypes = fixture.CreateMany<MediaType>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                mediaTypes[i].MediaTypeId = i + 1;
            }
            return mediaTypes;
        }

        private List<InvoiceLine> InvoiceLine()
        {
            var fixture = new Fixture();
            var invoiceLines = fixture.CreateMany<InvoiceLine>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                invoiceLines[i].InvoiceLineId = i + 1;
            }
            return invoiceLines;
        }

        private List<Invoice> Invoice()
        {
            var fixture = new Fixture();
            var invoices = fixture.CreateMany<Invoice>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                invoices[i].InvoiceId = i + 1;
            }
            return invoices;
        }

        private List<Genre> Genre()
        {
            var fixture = new Fixture();
            var genres = fixture.CreateMany<Genre>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                genres[i].GenreId = i + 1;
            }
            return genres;
        }

        private List<Customer> Customers()
        {
            var fixture = new Fixture();
            var customers = fixture.CreateMany<Customer>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                customers[i].CustomerId = i + 1;
            }
            return customers;
        }

        private List<Album> Albums()
        {
            var fixture = new Fixture();
            var albums = fixture.CreateMany<Album>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                albums[i].AlbumId = i + 1;
            }
            return albums;
        }

        private List<Artist> Artist()
        {
            var fixture = new Fixture();
            var artists = fixture.CreateMany<Artist>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                artists[i].ArtistId = i + 1;
            }
            return artists;
        }

        private List<Employee> Employee()
        {
            var fixture = new Fixture();
            var employees = fixture.CreateMany<Employee>(50).ToList();
            for (int i = 0; i < 50; i++)
            {
                employees[i].EmployeeId = i + 1;
            }
            return employees;
        }

    }
}
