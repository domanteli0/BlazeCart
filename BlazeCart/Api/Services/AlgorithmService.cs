using Models;

namespace Api.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        public int CheckIfContainsNumber(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsDigit(name[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public int CheckIfContainsWordStartingWithUpperNotFirst(string name)
        {
            name = name.Substring(1);
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public Item GetCheapestItemAlgorithm(Item comparedItem, List<Item> itemList)
        {
            List<Item> potentialCollection = new();
            foreach (var item in itemList)
            {
      
                if (item.NameLT.ToLower().Contains(comparedItem.NameLT.ToLower()))
                {
                    potentialCollection.Add(item);
                }
            }

            if (potentialCollection.Count == 1)
            {
                return potentialCollection[0];
            }
            if(potentialCollection.Count == 0)
            {
                return comparedItem;
            }
            else
            {
                double min = double.MaxValue;
                Item minItem = new Item();
                foreach (Item item in potentialCollection)
                {
                    if (item.Price < min && item.Ammount >= comparedItem.Ammount)
                    {
                        minItem.NameLT = item.NameLT;
                        minItem.Image = item.Image;
                        minItem.Ammount = item.Ammount;
                        minItem.Price = item.Price;
                        min = item.Price;
                    }
                }
                return minItem;
            }
        }
        /*
        public Dictionary<Item, string> GetItemDictionary(List<Item> _itemList)
        {
            Dictionary<Item, string> refactoredD = new Dictionary<Item, string>();
            foreach (Item item in _itemList)
            {
                if (item.NameLT == null)
                {
                    refactoredD.Add(item, "");
                }
                else
                {
                    string modified = refactorItemName(item.NameLT);
                    refactoredD.Add(item, modified.ToLower());
                }
            }
            return refactoredD;
        }
        */

        /*
        public HashSet<string> GetSetOfUnique(Dictionary<Item, string> refactoredD)
        {
            HashSet<string> uniqueSet = new HashSet<string>();
            foreach (String name in refactoredD.Values)
            {
                Console.WriteLine("Name tested:" + name);
                if (IsUnique(name))
                {
                    if (name.Length > 4)
                    {
                        uniqueSet.Add(name.ToLower());
                        Console.WriteLine("Token saved:" + name.ToLower());
                    }
                }
                else
                {
                    String[] tokensString = name.ToLower().Split(' ');
                    List<String> tokens = tokensString.ToList();
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        Console.WriteLine("Token:" + tokens[i]);
                        if (tokens[i].Length < 3)
                        {
                            tokens.RemoveAt(i);
                        }
                    }
                    if (tokens.Last().Length > 4)
                    {
                        uniqueSet.Add(tokens.Last().ToLower());
                        Console.WriteLine("Token saved:" + tokens.Last().ToLower());
                    }
                }

            }
            return uniqueSet;
        }
        */

        /*
        public bool IsUnique(string name)
        {
            string[] tokens = name.Split(' ');
            return tokens.Length == 1 ? true : false;
        }

        public Dictionary<Item, string> RefactorDictionaryToUnique(Dictionary<Item, string> refactoredD, HashSet<string> hs)
        {
            for (int i = 0; i < refactoredD.Count; i++)
            {
                for (int j = 0; j < hs.Count; j++)
                {
                    if (refactoredD.Values.ElementAt(i).ToLower().Contains(hs.ElementAt(j)))
                    {
                        refactoredD[refactoredD.Keys.ElementAt(i)] = hs.ElementAt(j);
                    }
                }
            }
            return refactoredD;
        }
        */

        public string refactorItemName(string? name)
        {
            if (name == null)
            {
                return "";
            }
            else
            {
                string modified = ThrowOutAllBrandNamesAndNumbers(name);
                modified = ThrowOutAllCommas(modified);
                modified = ThrowOutBrackets(modified);
                modified = ThrowOutAllNumbers(modified);
                modified = ThrowOutAllWordsNotFirstStartWithUpper(modified);
                return modified.Trim();
            }
        }

        public int returnFirstWhitespace(string substring)
        {
            for (int i = 0; i < substring.Length; i++)
            {
                if (char.IsWhiteSpace(substring[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public string ThrowOutAllBrandNamesAndNumbers(string nameLT)
        {
            for (int i = 1; i < nameLT.Length; i++)
            {
                if (Char.IsUpper(nameLT[i]))
                {
                    nameLT = nameLT.Remove(i, 1);
                    i--;
                }
            }
            return nameLT;
        }

        public string ThrowOutAllCommas(string name)
        {
            if (name.Contains(','))
            {
                int i = name.IndexOf(',');
                name = name.Remove(i);
            }
            if (name.Contains('.'))
            {
                int i = name.IndexOf('.');
                name = name.Remove(i);
            }
            return name;
        }

        public string ThrowOutAllNumbers(string name)
        {
            int start = CheckIfContainsNumber(name);
            if (start == -1)
            {
                return name;
            }
            else
            {
                string substring = name.Substring(start);
                int end = returnFirstWhitespace(substring);

                if (end == -1)
                {
                    name = name.Remove(start);
                }
                else
                {
                    name = name.Remove(start, end);
                }
            }
            return name;
        }

        public string ThrowOutAllWordsNotFirstStartWithUpper(string name)
        {
            int start = CheckIfContainsWordStartingWithUpperNotFirst(name);
            if (start != -1)
            {
                string substring = name.Substring(start);
                int end = returnFirstWhitespace(substring);

                if (end == -1)
                {
                    name = name.Remove(start);
                }
                else
                {
                    name = name.Remove(start, end);
                }

            }
            return name;
        }

        public string ThrowOutBrackets(string name)
        {
            int start;
            int end;
            while (name.Contains('('))
            {
                start = name.IndexOf('(');
                end = name.IndexOf(')');
                if (end == -1)
                {
                    return name.Remove(start);
                }
                else
                {
                    name = name.Remove(start, end - start + 1);
                }
            }
            return name;
        }
    }
}
