using Models;
using Common;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public abstract class ACategoryMap
	{
        private protected readonly ILogger _logger;
        public ACategoryMap(ILogger logger) { _logger = logger;  }

		private List<(string, List<(string, Category)>)> _map_store = new();
        private Category _unmappedCat = null;

        public void map_category(Category from, Category to)
		{
			foreach(var item in from.Items)
			{
				map_item(item, to);
			}
		}

		public void map_item(Item from, Category to)
		{
			from.Category = to;
			to.Items.Add(from);
			_logger
				.LogInformation(
					$"Mapped {from.NameLT} to {to.NameLT}"
				);
		}

		// TODO: better, non-frantic documentation
        /// <summary>
        /// Adds a rule
        /// </summary>
        /// <param name="categoryName"> category name FROM</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <param name="pattern_into_pairs"> pair of item pattern and category TO which ampped</param>
        private protected void addMapper(
			  string categoryName, List<(string, Category)> pattern_into_pairs
		)
		{
			_map_store.Add((categoryName, pattern_into_pairs));
		}

        /// <summary>
        /// Adds a category for which no sutable category for an item was found
        /// </summary>
        /// <param name="unmapCat"></param>
        private protected void addForUnmapped(Category unmapCat)
        {
            _unmappedCat = unmapCat;
        }

        /// <summary>
        /// Executes all rules in the order they were added
        /// </summary>
        /// <param name="categories"> WARNING!: ASUMES THAT CATEGORIES DON'T CONTAIN CHILDREN</param>
        /// <exception cref="NotImplementedException"></exception>
        private protected void executeMapper(IDictionary<string, Category> from)
		{
            if (_unmappedCat is null) throw new Exception("No category for unmatched items, use `addForUnmapped` (if you did, make sure `unmapCat` is not null)");

			foreach (var (catName, item_pattern_pairs) in _map_store)
			{
				var unmapped = new List<Item>();
                foreach (var item in from[catName].Items)
                {
                    foreach (var (item_pattern, to) in item_pattern_pairs)
                    {
                        if (item.NameLT.ToLower().ContainsPattern(item_pattern))
                        {
							unmapped.Remove(item);
                            item.Category = to;
                            to.Items.Add(item);

                            _logger
                                .LogInformation(
                                    $"Mapped {item.NameLT} to {to.NameLT}"
                                );
                        } else { unmapped.Add(item); }
                    }

                }

				foreach (var item in unmapped)
                {
                    item.Category = _unmappedCat;
                    _unmappedCat.Items.Add(item);
                }
            }
		}
	}
}

