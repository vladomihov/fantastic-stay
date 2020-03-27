using System;
using MediatR;

namespace FantasticStay.API.Application.Queries
{
	public class GetCalendarAvailabilityQuery : IRequest<GetCalendarAvailabilityViewModel>
	{
		public int PropertyId { get; }
		public DateTime From { get; }
		public DateTime To { get; }

		public GetCalendarAvailabilityQuery(int propertyId, DateTime @from, DateTime to)
		{
			PropertyId = propertyId;
			From = @from;
			To = to;
		}
	}
}
