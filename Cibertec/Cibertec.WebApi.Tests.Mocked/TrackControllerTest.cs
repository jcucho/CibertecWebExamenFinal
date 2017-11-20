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
    public class TrackControllerTest
    {
        private readonly TrackController _trackController;
        private readonly IUnitOfWork _uniMocked;

        public TrackControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _trackController = new TrackController(_uniMocked);
        }

        [Fact(DisplayName = "[TrackController] Get List")]
        public void Get_All_Test()
        {
            var result = _trackController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Track>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[TrackController] Insert")]
        public void Insert_Track_Test()
        {
            var track = new Track
            {
                TrackId = 3504,
                Name = "Test",
                AlbumId = 347,
                MediaTypeId = 2,
                GenreId = 10,
                Milliseconds = 100,
                UnitPrice=1
            };

            var result = _trackController.Post(track) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(3504);
        }

        [Fact(DisplayName = "[TrackController] Update")]
        public void Update_Track_Test()
        {
            var track = new Track
            {
                TrackId = 1,
                Name = "Test",
                AlbumId = 347,
                MediaTypeId = 2,
                GenreId = 10,
                Milliseconds = 100,
                UnitPrice = 1
            };

            var result = _trackController.Put(track) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The track is updated");

            var currentTrack = _uniMocked.Tracks.GetById(1);
            currentTrack.Should().NotBeNull();
            currentTrack.Name.Should().Be(track.Name);
            currentTrack.AlbumId.Should().Be(track.AlbumId);
            currentTrack.MediaTypeId.Should().Be(track.MediaTypeId);
            currentTrack.GenreId.Should().Be(track.GenreId);
            currentTrack.Milliseconds.Should().Be(track.Milliseconds);
            currentTrack.UnitPrice.Should().Be(track.UnitPrice);
        }

        [Fact(DisplayName = "[TrackController] Delete")]
        public void Delete_Track_Test()
        {
            var track = new Track
            {
                TrackId = 1
            };

            var result = _trackController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentTrack = _uniMocked.Tracks.GetById(1);
            currentTrack.Should().BeNull();
        }
    }
}
