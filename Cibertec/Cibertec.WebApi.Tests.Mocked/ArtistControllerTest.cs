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
    public class ArtistControllerTest
    {
        private readonly ArtistController _artistController;
        private readonly IUnitOfWork _uniMocked;

        public ArtistControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _artistController = new ArtistController(_uniMocked);
        }

        [Fact(DisplayName = "[ArtistController] Get List")]
        public void Get_All_Test()
        {
            var result = _artistController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Artist>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[ArtistController] Insert")]
        public void Insert_Artist_Test()
        {
            var artist = new Artist
            {
                ArtistId = 276,
                Name = "Test"
            };

            var result = _artistController.Post(artist) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(276);
        }

        [Fact(DisplayName = "[ArtistController] Update")]
        public void Update_Artist_Test()
        {
            var artist = new Artist
            {
                ArtistId = 1,
                Name = "Test"
            };

            var result = _artistController.Put(artist) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The artist is updated");

            var currentAistrt = _uniMocked.Artists.GetById(1);
            currentAistrt.Should().NotBeNull();
            currentAistrt.Name.Should().Be(artist.Name);
        }

        [Fact(DisplayName = "[ArtistController] Delete")]
        public void Delete_Artist_Test()
        {
            var artist = new Artist
            {
                ArtistId = 1
            };

            var result = _artistController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentArtist = _uniMocked.Artists.GetById(1);
            currentArtist.Should().BeNull();
        }
    }
}
