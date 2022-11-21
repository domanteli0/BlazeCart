using BlazeCart.Services;
using BlazeCart.ViewModels;
using FakeItEasy;

namespace Tests1.BlazeCart.ViewModels
{
    public class CartHistoryPageViewModelTest
    {
        private readonly CartPageViewModel _vm;
        private readonly DataService _dataService;

        public CartHistoryPageViewModelTest()
        {
            _dataService = A.Fake<DataService>();
            //_vm = new CartPageViewModel();
        }
    }
}
