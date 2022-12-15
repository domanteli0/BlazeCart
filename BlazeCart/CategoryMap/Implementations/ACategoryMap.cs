using System;
using Models;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public abstract class ACategoryMap
	{
		private protected readonly ILogger _logger;
        public ACategoryMap(ILogger logger) { _logger = logger;  }

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
	}
}

