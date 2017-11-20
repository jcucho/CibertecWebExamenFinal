using Cibertec.Mocked;
using Cibertec.Models;
using Cibertec.UnitOfWork;
using Cibertec.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cibertec.WebApi.Tests.Mocked
{
    public class AlbumControllerTest
    {
        private readonly AlbumController _albumController;
        private readonly IUnitOfWork _uniMocked;

        public AlbumControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _albumController = new AlbumController(_uniMocked);
        }

        [Fact(DisplayName = "[AlbumController] Get List")]
        public void Get_All_Test()
        {
            var result = _albumController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Album>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[AlbumController] Insert")]
        public void Insert_Album_Test()
        {
            var album = new Album
            {
                AlbumId = 348,
                Title = "Test",
                ArtistId = 275
            };

            var result = _albumController.Post(album) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(348);
        }

        [Fact(DisplayName = "[AlbumController] Update")]
        public void Update_Album_Test()
        {
            var album = new Album
            {
                AlbumId = 1,
                Title = "Test",
                ArtistId = 1
            };

            var result = _albumController.Put(album) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The album is updated");

            var currentAlbum = _uniMocked.Albums.GetById(1);
            currentAlbum.Should().NotBeNull();
            currentAlbum.AlbumId.Should().Be(album.AlbumId);
            currentAlbum.Title.Should().Be(album.Title);
            currentAlbum.ArtistId.Should().Be(album.ArtistId);            
        }

        [Fact(DisplayName = "[AlbumController] Delete")]
        public void Delete_Album_Test()
        {
            var album = new Album
            {
                AlbumId = 1
            };

            var result = _albumController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentAlbum = _uniMocked.Albums.GetById(1);
            currentAlbum.Should().BeNull();
        }
    }
}
