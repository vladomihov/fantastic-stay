using System.Collections.Generic;
using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public class Price : ValueObject
	{
		public decimal Amount { get; }

		public int CurrencyId { get; }

		public Price(decimal amount, int currencyId)
		{
			Amount = amount;
			CurrencyId = currencyId;
		}

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Amount;
			yield return CurrencyId;
		}
	}
}