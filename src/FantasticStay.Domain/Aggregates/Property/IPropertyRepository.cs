using System.Threading.Tasks;
using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Property
{
	public interface IPropertyRepository : IRepository<Property>
	{
		Property Add(Property property);

		void Update(Property property);

		Task<Property> GetAsync(int propertyId);
	}
}
