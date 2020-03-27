using MediatR;

namespace FantasticStay.API.Application.Commands
{
	public class CreateCalendarCommand : IRequest<bool>
	{
		public int PropertyId { get; }

		public CreateCalendarCommand(int propertyId)
		{
			PropertyId = propertyId;
		}
	}
}
