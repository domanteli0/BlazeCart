using BlazeCart.Services;
using BlazeCart.ViewModels;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace Tests1.BlazeCart.ViewModels
{
    public class CartHistoryPageViewModelTest
    {
        private readonly CartHistoryPageViewModel _vm;
        private readonly DataService _dataService;
        private readonly ItemService _itemService;
        private readonly Logger<CartHistoryPageViewModel> _logger;

        public CartHistoryPageViewModelTest()
        {
            _dataService = A.Fake<DataService>();
            _itemService = A.Fake<ItemService>();
            _logger = A.Fake<Logger<CartHistoryPageViewModel>>();
            _vm = new CartHistoryPageViewModel(_dataService, _itemService, _logger);
        }
    }
}
