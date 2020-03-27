using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace FantasticStay.Infrastructure.Repositories
{
	public class CalendarRepository : ICalendarRepository
	{
		private readonly FantasticStayContext _context;

		public IUnitOfWork UnitOfWork => _context;

		public CalendarRepository(FantasticStayContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public Calendar Add(Calendar calendar)
		{
			return _context.Calendars.Add(calendar).Entity;
		}

		public void Update(Calendar calendar)
		{
			_context.Entry(calendar).State = EntityState.Modified;
		}

		public async Task<Calendar> GetByPropertyAsync(int propertyId)
		{
			var calendar = await _context.Calendars.FirstOrDefaultAsync(o => o.PropertyId == propertyId)
						   ?? _context.Calendars.Local.FirstOrDefault(o => o.PropertyId == propertyId);

			if (calendar != null)
			{
				await _context.Entry(calendar)
					.Collection(i => i.Items).LoadAsync();
			}

			return calendar;
		}
	}
}
