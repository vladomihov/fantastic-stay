using FantasticStay.Domain.SeedWork;

namespace FantasticStay.Domain.Aggregates.Calendar
{
	public class CalendarItemState : Enumeration<CalendarItemState>
	{
		public static readonly CalendarItemState Blocked = new CalendarItemState(1, nameof(Blocked));
		public static readonly CalendarItemState Available = new CalendarItemState(2, nameof(Available));

		private CalendarItemState(int id, string name) : base(id, name) { }
	}
}