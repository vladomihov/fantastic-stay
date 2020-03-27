using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Exceptions;
using MediatR;

namespace FantasticStay.API.Application.Queries
{
	public class GetCalendarAvailabilityQueryHandler : IRequestHandler<GetCalendarAvailabilityQuery, GetCalendarAvailabilityViewModel>
	{
		private readonly ICalendarRepository _calendarRepository;

		public GetCalendarAvailabilityQueryHandler(ICalendarRepository calendarRepository)
		{
			_calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
		}

		public async Task<GetCalendarAvailabilityViewModel> Handle(GetCalendarAvailabilityQuery query, CancellationToken cancellationToken)
		{
			var calendar = await _calendarRepository.GetByPropertyAsync(query.PropertyId);
			if (calendar == null)
			{
				throw new FantasticStayDomainException($"Cannot find calendar for property with id '{query.PropertyId}'.");
			}

			var availability = calendar.GetAvailability(query.From, query.To);
			return new GetCalendarAvailabilityViewModel(query.PropertyId, query.From, query.To, availability);
		}
	}
}