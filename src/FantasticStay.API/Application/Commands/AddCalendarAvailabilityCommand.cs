using System;
using MediatR;

namespace FantasticStay.API.Application.Commands
{
	public class AddCalendarAvailabilityCommand : IRequest<bool>
	{
		public int PropertyId { get; }

		public DateTime From { get; }
		public DateTime To { get; }

		public decimal Amount { get; }
		public int CurrencyId { get; }

		public AddCalendarAvailabilityCommand(int propertyId, DateTime @from, DateTime to, decimal amount, int currencyId)
		{
			PropertyId = propertyId;
			From = @from;
			To = to;
			Amount = amount;
			CurrencyId = currencyId;
		}
	}
}
