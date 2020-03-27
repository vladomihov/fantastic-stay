using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Exceptions;
using MediatR;

namespace FantasticStay.API.Application.Commands
{
	public class AddCalendarAvailabilityCommandHandler : IRequestHandler<AddCalendarAvailabilityCommand, bool>
	{
		private readonly ICalendarRepository _calendarRepository;

		public AddCalendarAvailabilityCommandHandler(ICalendarRepository calendarRepository)
		{
			_calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
		}

		public async Task<bool> Handle(AddCalendarAvailabilityCommand command, CancellationToken cancellationToken)
		{
			var calendar = await _calendarRepository.GetByPropertyAsync(command.PropertyId);
			if (calendar == null)
			{
				throw new FantasticStayDomainException($"Cannot find calendar for property with id '{command.PropertyId}'.");
			}

			var price = new Price(command.Amount, command.CurrencyId);
			calendar.AddAvailability(command.From, command.To, price);

			_calendarRepository.Update(calendar);

			return await _calendarRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		}
	}
}
