using System;
using System.Collections.Generic;
using System.Linq;
using FantasticStay.Domain.Aggregates.Calendar.StateSettings;
using FantasticStay.Domain.Exceptions;
using FantasticStay.Domain.SeedWork;
using Newtonsoft.Json;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public class Calendar : Entity, IAggregateRoot
	{

		public int PropertyId { get; }

		private List<CalendarItem> _items;
		public IReadOnlyCollection<CalendarItem> Items => _items;

		public Calendar(int propertyId)
		{
			if (propertyId == default)
			{
				throw new FantasticStayDomainException("Cannot create calendar with an empty property Id.");
			}

			PropertyId = propertyId;

			_items = new List<CalendarItem>();
		}

		public void BlockDates(DateTime from, DateTime to)
		{
			var settings = new BlockedStateSettings();
			AddItem(from, to, CalendarItemState.Blocked, settings);
		}

		public void AddAvailability(DateTime from, DateTime to, Price price)
		{
			if (price == null)
			{
				throw new FantasticStayDomainException("Cannot add availability with no price.");
			}

			if (price.Amount < 0)
			{
				throw new FantasticStayDomainException("Cannot add availability with negative price.");
			}

			var a = CalendarItemState.Get(price.CurrencyId);

			if (Currency.Get(price.CurrencyId) == null)
			{
				throw new FantasticStayDomainException($"Currency with id '{price.CurrencyId}' is not supported.");
			}

			var settings = new AvailableStateSettings(price);
			AddItem(from, to, CalendarItemState.Available, settings);
		}

		private void AddItem(DateTime from, DateTime to, CalendarItemState state, object settings)
		{
			CheckFromTo(from, to);

			var statePayload = JsonConvert.SerializeObject(settings);
			var item = new CalendarItem(from, to, state.Id, statePayload, DateTime.UtcNow);
			_items.Add(item);
		}

		public IDictionary<DateTime, Price> GetAvailability(DateTime @from, DateTime to)
		{
			CheckFromTo(from, to);

			var items = Items.OrderByDescending(item => item.CreatedOn).ToList();

			var availability = new Dictionary<DateTime, Price>();
			for (var date = from.Date; date <= to; date = date.AddDays(1))
			{
				var dateItem = items.FirstOrDefault(item => date >= item.From && date <= item.To);
				if (dateItem != null && dateItem.StateId == CalendarItemState.Available.Id)
				{
					var dateSettings = JsonConvert.DeserializeObject<AvailableStateSettings>(dateItem.StatePayload);
					availability.Add(date, dateSettings.Price);
				}
			}

			return availability;
		}

		private void CheckFromTo(DateTime @from, DateTime to)
		{
			if (from > to)
			{
				throw new FantasticStayDomainException($"Parameter 'from' ({from}) cannot be after parameter 'to' ({to})");
			}
		}
	}
}