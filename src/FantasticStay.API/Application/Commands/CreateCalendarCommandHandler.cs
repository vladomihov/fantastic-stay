using System;
using System.Threading;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Aggregates.Property;
using FantasticStay.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FantasticStay.API.Application.Commands
{
	public class CreateCalendarCommandHandler : IRequestHandler<CreateCalendarCommand, bool>
	{
		private readonly ICalendarRepository _calendarRepository;
		private readonly IPropertyRepository _propertyRepository;
		private readonly ILogger<CreateCalendarCommandHandler> _logger;

		public CreateCalendarCommandHandler(ICalendarRepository calendarRepository, IPropertyRepository propertyRepository, ILogger<CreateCalendarCommandHandler> logger)
		{
			_calendarRepository = calendarRepository ?? throw new ArgumentNullException(nameof(calendarRepository));
			_propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> Handle(CreateCalendarCommand command, CancellationToken cancellationToken)
		{
			var property = await _propertyRepository.GetAsync(command.PropertyId);
			if (property == null)
			{
				throw new FantasticStayDomainException($"Cannot find property with id '{command.PropertyId}'.");
			}

			var calendar = await _calendarRepository.GetByPropertyAsync(property.Id);
			if (calendar != null)
			{
				return true;
			}

			calendar = new Calendar(property.Id);

			_logger.LogInformation("Creating Calendar - {@Calendar}", calendar);

			_calendarRepository.Add(calendar);

			return await _calendarRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		}
	}
}
