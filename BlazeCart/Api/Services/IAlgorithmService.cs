using Models;

namespace Api.Services
{
    public interface IAlgorithmService
    {
       //Dictionary<Item, string> GetItemDictionary(List<Item> _itemList);
        string refactorItemName(string? name);
        Item GetCheapestItemAlgorithm(Item comparedItem, List<Item> itemList);

        string ThrowOutAllBrandNamesAndNumbers(string nameLT);
        string ThrowOutAllCommas(string name);

        string ThrowOutAllWordsNotFirstStartWithUpper(string name);

        int CheckIfContainsWordStartingWithUpperNotFirst(string name);

        string ThrowOutBrackets(string name);

        string ThrowOutAllNumbers(string name);

        int returnFirstWhitespace(string substring);
        //Boolean IsUnique(string name);

        //HashSet<string> GetSetOfUnique(Dictionary<Item, string> refactoredD);

        int CheckIfContainsNumber(string name);

        //Dictionary<Item, string> RefactorDictionaryToUnique(Dictionary<Item, string> refactoredD, HashSet<String> hs);
    }
}
