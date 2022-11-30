using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;
using FakeItEasy;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Tests1.BlazeCart.Services
{

    public class SliderFunctionTest
    {
        private readonly SliderService _sut;
        private readonly ItemService _itemService;
        private static IList<ObservableCollection<Item>> _testData;
        private readonly string[] _fileNames = new string[] { "shopItems.json", "shopItems1.json", "shopItems2.json" };


        public SliderFunctionTest()
        {
           _testData = A.CollectionOfFake<ObservableCollection<Item>>(3);
           
            _sut = new SliderService();
            _itemService = new ItemService();

        }

        [Theory]
        [MemberData(nameof(MaximumTestData))]
        public void ReturnMaximumValueTest(double expected, ObservableCollection<Item> testItems)
        {
            Assert.Equal(expected, _sut.GetMaximum(testItems));
        }

        public static IEnumerable<object[]> MaximumTestData()
        {
            yield return new object[] { 5.84, _testData[0] };
            yield return new object[] { 3.70, _testData[1] };
            yield return new object[] { 3.10, _testData[2] };
        }

        [Theory]
        [MemberData(nameof(MinimumTestData))]
        public void ReturnMinimumValueTest(double expected, ObservableCollection<Item> testItems)
        {
            Assert.Equal(expected, _sut.GetMinimum(testItems));
        }

        public static IEnumerable<object[]> MinimumTestData()
        {
            yield return new object[] { 1.05, _testData[0] };
            yield return new object[] { 1.59, _testData[1] };
            yield return new object[] { 3.10, _testData[2] };
        }

    }
}
