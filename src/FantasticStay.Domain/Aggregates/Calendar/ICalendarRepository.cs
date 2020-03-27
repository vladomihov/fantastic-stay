using System.Threading.Tasks;
using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public interface ICalendarRepository : IRepository<Calendar>
	{
		Calendar Add(Calendar calendar);

		void Update(Calendar calendar);

		Task<Calendar> GetByPropertyAsync(int propertyId);
	}
}
