using Api.Controllers;
using Api.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Models;
using FluentAssertions;

namespace Tests1.Api.Controllers
{
    public class CategoryRepositoryTest
    {
        private ICategoryRepository _mockRepository;
        public CategoryRepositoryTest()
        {
            _mockRepository = A.Fake<ICategoryRepository>();
        }

        [Fact]
        public void GetAllCategoriesTest()
        {
            var controller = new CategoryController(_mockRepository);

            var result = controller.GetAllCategories();
            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
