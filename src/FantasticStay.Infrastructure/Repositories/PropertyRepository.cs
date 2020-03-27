using System;
using System.Linq;
using System.Threading.Tasks;
using FantasticStay.Domain.Aggregates.Property;
using FantasticStay.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace FantasticStay.Infrastructure.Repositories
{
	public class PropertyRepository : IPropertyRepository
	{
		private readonly FantasticStayContext _context;

		public IUnitOfWork UnitOfWork => _context;

		public PropertyRepository(FantasticStayContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public Property Add(Property property)
		{
			return _context.Properties.Add(property).Entity;
		}

		public void Update(Property property)
		{
			_context.Entry(property).State = EntityState.Modified;
		}

		public async Task<Property> GetAsync(int propertyId)
		{
			var property = await _context.Properties.FindAsync(propertyId)
						   ?? _context.Properties.Local.FirstOrDefault(o => o.Id == propertyId);

			return property;
		}
	}
}
