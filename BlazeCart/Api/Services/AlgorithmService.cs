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

        public Item GetCheapestItemAlgorithm(Item comparedItem, List<Item> itemList, string oldName)
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
                comparedItem.NameLT = oldName;
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
                        minItem.Merch = item.Merch;
                        min = item.Price;
                    }
                }
                return minItem;
            }
        }

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
