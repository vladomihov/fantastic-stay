namespace FantasticStay.Domain.Aggregates.Calendar.StateSettings
{
	public class AvailableStateSettings
	{
		public Price Price { get; }

		public AvailableStateSettings(Price price)
		{
			Price = price;
		}
	}
}
