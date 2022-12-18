using Api.Controllers;
using Api.Repositories;
using Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace Tests1.Api.Controllers
{
    public class ItemControllerTest
    {
        private readonly ItemController _controller;
        private readonly IItemRepository _mockRepository;

        public ItemControllerTest()
        {
            _mockRepository = A.Fake<IItemRepository>();
            _controller = new ItemController(_mockRepository);

        }

        [Fact]
        public async Task GetRangeOfItemsTest()
        {
            int count = 5;
            int index = 0;
            var fakeItems = A.CollectionOfDummy<Item>(count).AsEnumerable();
            A.CallTo(() => _mockRepository.GetRangeOfItemsAsync(index, count)).Returns(Task.FromResult(fakeItems));

            var actionResult = await _controller.GetRangeOfItems(index, count);

            var result = actionResult as OkObjectResult;
            var returnItems = result.Value as IEnumerable<Item>;
            Assert.NotNull(returnItems);
            Assert.Equal(count, returnItems.Count());

        }
    }
}
