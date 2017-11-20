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
    public class MediaTypeControllerTest
    {
        private readonly MediaTypeController _mediaTypeController;
        private readonly IUnitOfWork _uniMocked;

        public MediaTypeControllerTest()
        {
            var unitMocked = new UnitOfWorkMocked();
            _uniMocked = unitMocked.GetInstance();
            _mediaTypeController = new MediaTypeController(_uniMocked);
        }

        [Fact(DisplayName = "[MediaTypeController] Get List")]
        public void Get_All_Test()
        {
            var result = _mediaTypeController.GetList() as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            var model = result.Value as List<MediaType>;
            model.Count.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[MediaTypeController] Insert")]
        public void Insert_MediaType_Test()
        {
            var mediaType = new MediaType
            {
                MediaTypeId = 6,
                Name = "Test"
            };

            var result = _mediaTypeController.Post(mediaType) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToInt32(result.Value);
            model.Should().Be(6);
        }

        [Fact(DisplayName = "[MediaTypeController] Update")]
        public void Update_MediaType_Test()
        {
            var mediaType = new MediaType
            {
                MediaTypeId = 1,
                Name = "Test"
            };

            var result = _mediaTypeController.Put(mediaType) as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = result.Value?.GetType().GetProperty("Message").GetValue(result.Value);
            model.Should().Be("The media type is updated");

            var currentMediaType = _uniMocked.MediaTypes.GetById(1);
            currentMediaType.Should().NotBeNull();
            currentMediaType.Name.Should().Be(mediaType.Name);

        }

        [Fact(DisplayName = "[MediaTypeController] Delete")]
        public void Delete_MediaType_Test()
        {
            var mediaType = new MediaType
            {
                MediaTypeId = 1
            };

            var result = _mediaTypeController.Delete(1) as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            var model = Convert.ToBoolean(result.Value);
            model.Should().BeTrue();

            var currentMediaType = _uniMocked.MediaTypes.GetById(1);
            currentMediaType.Should().BeNull();
        }
    }
}
