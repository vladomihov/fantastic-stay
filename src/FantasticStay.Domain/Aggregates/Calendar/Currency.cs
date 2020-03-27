using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public class Currency : Enumeration<Currency>
	{
		public static readonly Currency EUR = new Currency(1, "Euro", nameof(EUR));
		public static readonly Currency USD = new Currency(2, "US Dollar", nameof(USD));
		public static readonly Currency BGN = new Currency(3, "Bulgarian Lev", nameof(BGN));

		public string Code { get; }

		private Currency(int id, string name, string code)
			: base(id, name)
		{
			Code = code;
		}
	}
}