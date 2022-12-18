using Api.Controllers;
using Api.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Models;
using FluentAssertions;

namespace Tests1.Api.Controllers
{
    public class CategoryControllerTest
    {
        private readonly ICategoryRepository _mockRepository;
        private readonly CategoryController _controller;
        private readonly Guid _categoryId;

        public CategoryControllerTest()
        {
            _mockRepository = A.Fake<ICategoryRepository>();
            _controller = new CategoryController(_mockRepository);
            _categoryId = Guid.NewGuid();
        }

        [Fact]
        public void GetAllCategoriesTest()
        {

            var result = _controller.GetAllCategories();
            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public void GetCategoryById()
        {

           // var result = _controller.GetCategoryByIdAsync(_categoryId);

           // Assert.Null(okResult);
          //  Assert.Equal(200, okResult.StatusCode);
        }
    }
}
