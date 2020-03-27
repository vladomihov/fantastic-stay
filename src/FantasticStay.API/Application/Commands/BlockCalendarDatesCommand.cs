using System;
using MediatR;

namespace FantasticStay.API.Application.Commands
{
	public class BlockCalendarDatesCommand : IRequest<bool>
	{
		public int PropertyId { get; }
		public DateTime From { get; }
		public DateTime To { get; }

		public BlockCalendarDatesCommand(int propertyId, DateTime @from, DateTime to)
		{
			PropertyId = propertyId;
			From = @from;
			To = to;
		}
	}
}
