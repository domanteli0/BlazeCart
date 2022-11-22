using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;

namespace Tests1.BlazeCart.Services
{

    public class SliderFunctionTest
    {
        private readonly SliderService _sut;
        private readonly ItemService _itemService;
        private static ObservableCollection<Item>[] testData = new ObservableCollection<Item>[3];
        private readonly string[] _fileNames = new string[] { "shopItems.json", "shopItems1.json", "shopItems2.json" };


        public SliderFunctionTest()
        {
            _sut = new SliderService();
            _itemService = new ItemService();
            for (int i = 0; i < _fileNames.Count(); i++)
            {
                testData[i] = _itemService.GetItems(_fileNames[i]).Result;
            }
        }

        [Theory]
        [MemberData(nameof(MaximumTestData))]
        public void ReturnMaximumValueTest(double expected, ObservableCollection<Item> testItems)
        {
            Assert.Equal(expected, _sut.GetMaximum(testItems));
        }

        public static IEnumerable<object[]> MaximumTestData()
        {
            yield return new object[] { 5.84, testData[0] };
            yield return new object[] { 3.70, testData[1] };
            yield return new object[] { 3.10, testData[2] };
        }

        [Theory]
        [MemberData(nameof(MinimumTestData))]
        public void ReturnMinimumValueTest(double expected, ObservableCollection<Item> testItems)
        {
            Assert.Equal(expected, _sut.GetMinimum(testItems));
        }

        public static IEnumerable<object[]> MinimumTestData()
        {
            yield return new object[] { 1.05, testData[0] };
            yield return new object[] { 1.59, testData[1] };
            yield return new object[] { 3.10, testData[2] };
        }

    }
}
