using System;
using System.Collections.Generic;
using FantasticStay.Domain.Aggregates.Calendar;

namespace FantasticStay.API.Application.Queries
{
	public class GetCalendarAvailabilityViewModel
	{
		public int PropertyId { get; }
		public DateTime From { get; }
		public DateTime To { get; }

		public IDictionary<DateTime, Price> Dates { get; }

		public GetCalendarAvailabilityViewModel(int propertyId, DateTime @from, DateTime to, IDictionary<DateTime, Price> dates)
		{
			PropertyId = propertyId;
			From = @from;
			To = to;
			Dates = dates;
		}
	}
}
