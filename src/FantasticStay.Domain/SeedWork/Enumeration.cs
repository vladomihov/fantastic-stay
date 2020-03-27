using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FantasticStay.Domain.SeedWork
{
	public abstract class Enumeration<TItem>
		where TItem : Enumeration<TItem>
	{
		public static IEnumerable<TItem> All => _all ??= ExtractItems();
		private static IEnumerable<TItem> _all;

		private static IEnumerable<TItem> ExtractItems()
		{
			var items = typeof(TItem)
				.GetFields(BindingFlags.Static | BindingFlags.Public)
				.Where(x => x.FieldType == typeof(TItem))
				.Select(x => x.GetValue(null))
				.Cast<TItem>()
				.ToArray();

			var duplicates = items.GroupBy(item => item.Id).Where(group => group.Count() > 1).ToList();
			if (duplicates.Any())
			{
				var duplicatedIds = string.Join(", ", duplicates.Select(group => group.Key));
				throw new InvalidOperationException($"Type '{typeof(TItem).Name}' cannot have items with duplicate Id. Duplicated Ids: { duplicatedIds}.");
			}

			return items;
		}

		public static TItem Get(int id)
		{
			return All.FirstOrDefault(item => item.Id == id);
		}

		public int Id { get; }

		public string Name { get; }

		protected Enumeration(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public override string ToString() => Name;
	}
}
