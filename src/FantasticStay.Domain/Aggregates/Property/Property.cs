using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Property
{
	public class Property : Entity, IAggregateRoot
	{
		public Address Address { get; private set; }

		public Property()
		{ }

		public Property(Address address)
		{
			Address = address;
		}
	}
}