using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace Tests1.BlazeCart.ViewModels
{
    public class CartPageViewModelTest
    {
        private readonly Item _item1 = new();
        private readonly Item _item2 = new();
        private readonly ObservableCollection<Item> _cartItems;
        private readonly CartPageViewModel _vm;
        private readonly DataService _dataService;
        private readonly ItemService _itemService;
        private readonly Logger<CartPageViewModel> _logger;

        public CartPageViewModelTest()
        {
            //MOCKING
            _dataService = A.Fake<DataService>();
            _itemService = A.Fake<ItemService>();
            _logger = A.Fake<Logger<CartPageViewModel>>();
            _vm = new CartPageViewModel(_dataService, _itemService, _logger);

            _item1.Price = 5.67;
            _item2.Price = 3.14;
            _item1.Quantity = 2;
            _item2.Quantity = 3;
            _cartItems = new ObservableCollection<Item>
            {
                _item1, _item2
            };
        }

        [Fact]
        public void GetCartPriceTest()
        {
            var actual = _vm.GetCartPrice(_cartItems);
            var expected = CalculateExpectedPrice(_cartItems);

            actual.Should().Be(expected);
        }

        public double CalculateExpectedPrice(ObservableCollection<Item> cartItems)
        {
            return cartItems.Sum(x => x.Price * x.Quantity);
        }

        [Fact]
        public void GetCartItemsCountTest()
        {
            var expected = 0;
            foreach (var i in _cartItems)
            {
                expected += i.Quantity;
            }

            var actual = _vm.GetCartItemsCount(_cartItems);

            actual.Should().Be(expected);

        }

        [Fact]
        public void AddQuantityTest()
        {
            var quantity1 = _item1.Quantity;
            var quantity2 = _item2.Quantity;
            var expected1 = ++ quantity1;
            var expected2 = ++ quantity2;

            _vm.AddQuantityCommand.Execute(_item1);
            _vm.AddQuantityCommand.Execute(_item2);
            var actual1 = _item1.Quantity;
            var actual2 = _item2.Quantity;

            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
        }

        [Fact]
        public void RemoveQuantityTest()
        {
            var quantity1 = _item1.Quantity;
            var quantity2 = _item2.Quantity;
            var expected1 = --quantity1;
            var expected2 = --quantity2;

            _vm.RemoveQuantityCommand.Execute(_item1);
            _vm.RemoveQuantityCommand.Execute(_item2);
            var actual1 = _item1.Quantity;
            var actual2 = _item2.Quantity;

            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
        }
    }
}
