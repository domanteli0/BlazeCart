using Models;
using Common;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public abstract class ACategoryMap
	{
        private protected readonly ILogger _logger;
        public ACategoryMap(ILogger logger) { _logger = logger;  }

		public List<(string, List<(string, Category)>)> _map_store = new();
        private Category? _unmappedCat = null;

        public void map_category(Category from, Category to)
		{
            from.Items.ForEach(item => map_item(item, to));
		}

		public void map_item(Item from, Category to)
		{
			from.Category = to;
			to.Items.Add(from);
			_logger.LogInformation(
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
			  string categoryName
            , List<(string, Category)> pattern_into_pairs
		) =>
            _map_store.Add((categoryName, pattern_into_pairs));

        /// <summary>
        /// Adds a category for which no sutable category for an item was found
        /// </summary>
        /// <param name="unmapCat"></param>
        private protected void addForUnmapped(Category unmapCat) =>
            _unmappedCat = unmapCat;

        /// <summary>
        /// Executes all rules from `addMapper` in the order they were added
        /// NOTE: This method will remove `Item`s from it's `Category`
        /// Thus if one of your `Category`ies has a null assigned to field `Items`
        /// it is very likely that a NullException will be thrown
        /// </summary>
        /// <param name="categories"> WARNING!: ASUMES THAT CATEGORIES DON'T CONTAIN CHILDREN</param>
        /// <exception cref="NotImplementedException"></exception>
        // TODO: Figure out why unmapped isn't saved to the DB
        private protected void executeMapper(List<Category> from)
		{
            if (_unmappedCat is null) throw new Exception("No category for unmatched items, use `addForUnmapped` (if you did, make sure `unmapCat` is not null)");

            // assume unmapped
            from
                .GetWithoutChildren()
                .SelectMany(c => c.Items)
                .ToList()
                .ForEach(i => {
                    i.Category = _unmappedCat;
                    _unmappedCat.Items.Add(i);
                });

            // then check if mapped
            foreach (var (catName, item_pattern_to_pairs) in _map_store)
            {
                foreach (
                    var item in
                    from
                        .FindAll(c => c.NameLT!.Equals(catName))
                        .SelectMany(c => c.Items))
                {
                    foreach (var (item_pattern, to) in item_pattern_to_pairs)
                    {
                        if (item.NameLT.ToLower().ContainsPattern(item_pattern))
                        {
                            try
                            {
                                _unmappedCat.Items.Remove(item);
                                item.Category = to;
                                to.Items.Add(item);

                                _logger
                                    .LogInformation(
                                        $"Mapped {item.NameLT} to {to.NameLT}"
                                    );

                            }
                            catch (Exception)
                            {
                                _logger.LogError($"Exception was thrown; '{item}' in '{item.Category.ToStringNullSafe()}'");
                                throw;
                            }
                        }
                    }
                }
            }
        }
	}
}

