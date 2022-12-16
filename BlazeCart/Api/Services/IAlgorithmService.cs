﻿using Models;

namespace Api.Services
{
    public interface IAlgorithmService
    {
        string refactorItemName(string? name);
        Item GetCheapestItemAlgorithm(Item comparedItem, List<Item> itemList);

        string ThrowOutAllBrandNamesAndNumbers(string nameLT);
        string ThrowOutAllCommas(string name);

        string ThrowOutAllWordsNotFirstStartWithUpper(string name);

        int CheckIfContainsWordStartingWithUpperNotFirst(string name);

        string ThrowOutBrackets(string name);

        string ThrowOutAllNumbers(string name);

        int returnFirstWhitespace(string substring);
      
        int CheckIfContainsNumber(string name);

    }
}
