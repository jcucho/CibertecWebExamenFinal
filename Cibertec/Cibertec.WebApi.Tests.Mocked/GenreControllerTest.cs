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
    public class GenreControllerTest
    {
        private readonly GenreController _genreController;
        private readonly IUnitOfWork _uniMocked;

        public GenreControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _genreController = new GenreController(_uniMocked);
        }

        [Fact(DisplayName = "[GenreController] Get List")]
        public void Get_All_Test()
        {
            var result = _genreController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<Genre>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[GenreController] Insert")]
        public void Insert_Artist_Test()
        {
            var genre = new Genre
            {
                GenreId = 26,
                Name = "Test"
            };

            var result = _genreController.Post(genre) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(26);
        }

        [Fact(DisplayName = "[GenreController] Update")]
        public void Update_Artist_Test()
        {
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Test"
            };

            var result = _genreController.Put(genre) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The genres is updated");

            var currentGenre = _uniMocked.Genres.GetById(1);
            currentGenre.Should().NotBeNull();
            currentGenre.Name.Should().Be(genre.Name);
        }

        [Fact(DisplayName = "[GenreController] Delete")]
        public void Delete_Artist_Test()
        {
            var genre = new Genre
            {
                GenreId = 1
            };

            var result = _genreController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentGenre = _uniMocked.Genres.GetById(1);
            currentGenre.Should().BeNull();
        }
    }
}
