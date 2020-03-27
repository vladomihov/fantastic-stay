using System;
using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public class CalendarItem : Entity
	{
		public CalendarItem(DateTime @from, DateTime to, int stateId, string statePayload, DateTime createdOn)
		{
			From = @from;
			To = to;
			StateId = stateId;
			StatePayload = statePayload;
			CreatedOn = createdOn;
		}

		public DateTime From { get; }

		public DateTime To { get; }

		public int StateId { get; }

		public string StatePayload { get; }

		public DateTime CreatedOn { get; }
	}
}