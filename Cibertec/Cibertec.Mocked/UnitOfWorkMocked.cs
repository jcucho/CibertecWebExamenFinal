using Ploeh.AutoFixture;
using System.Collections.Generic;
using System;
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
        
        public UnitOfWorkMocked()
        {
            _customers = Customers();
            _albums = Albums();
        }

        public IUnitOfWork GetInstance()
        {
            var mocked = new Mock<IUnitOfWork>();
            mocked.Setup(u => u.Customers).Returns(CustomerRepositoryMocked());
            mocked.Setup(u => u.Albums).Returns(AlbumRepositoryMocked());
            return mocked.Object;
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
    }
}
