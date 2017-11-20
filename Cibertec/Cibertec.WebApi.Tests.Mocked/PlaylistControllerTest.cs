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
    public class PlaylistControllerTest
    {
        private readonly PlaylistController _playlistController;
        private readonly IUnitOfWork _uniMocked;

        public PlaylistControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _playlistController = new PlaylistController(_uniMocked);
        }

        [Fact(DisplayName = "[PlaylistController] Get List")]
        public void Get_All_Test()
        {
            var result = _playlistController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Playlist>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[PlaylistController] Insert")]
        public void Insert_Playlist_Test()
        {
            var playlist = new Playlist
            {
                PlaylistId = 19,
                Name = "Test"
            };

            var result = _playlistController.Post(playlist) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(19);
        }

        [Fact(DisplayName = "[PlaylistController] Update")]
        public void Update_Playlist_Test()
        {
            var playlist = new Playlist
            {
                PlaylistId = 1,
                Name = "Test"
            };

            var result = _playlistController.Put(playlist) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The playlist is updated");

            var currentPlaylist = _uniMocked.Playlists.GetById(1);
            currentPlaylist.Should().NotBeNull();
            currentPlaylist.Name.Should().Be(playlist.Name);
        }

        [Fact(DisplayName = "[PlaylistController] Delete")]
        public void Delete_Playlist_Test()
        {
            var playlist = new Playlist
            {
                PlaylistId = 1
            };

            var result = _playlistController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentPlaylist = _uniMocked.Playlists.GetById(1);
            currentPlaylist.Should().BeNull();
        }
    }
}
