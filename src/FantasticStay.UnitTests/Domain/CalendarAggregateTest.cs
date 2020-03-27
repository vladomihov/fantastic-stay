using System;
using System.Collections.Generic;
using System.Linq;
using FantasticStay.Domain.Aggregates.Calendar;
using FantasticStay.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FantasticStay.UnitTests.Domain
{
	[TestClass]
	public class CalendarAggregateTest
	{
		[TestMethod]
		public void Create_calendar_success()
		{
			var calendar = new Calendar(1);

			Assert.IsNotNull(calendar);
		}

		[TestMethod]
		public void Create_calendar_invalid_property()
		{
			Assert.ThrowsException<FantasticStayDomainException>(() => new Calendar(0));
		}

		[TestMethod]
		public void Add_availability_zero_price_success()
		{
			var calendar = new Calendar(1);
			var price = new Price(0, Currency.EUR.Id);

			calendar.AddAvailability(new DateTime(2020, 3, 1), new DateTime(2020, 3, 3), price);

			Assert.AreEqual(1, calendar.Items.Count);
		}

		[TestMethod]
		public void Add_availability_invalid_price_null()
		{
			var calendar = new Calendar(1);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.AddAvailability(new DateTime(2020, 3, 1), new DateTime(2020, 3, 3), null));
		}

		[TestMethod]
		public void Add_availability_invalid_price_amount()
		{
			var calendar = new Calendar(1);
			var price = new Price(-1, Currency.EUR.Id);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.AddAvailability(new DateTime(2020, 3, 1), new DateTime(2020, 3, 3), price));
		}

		[TestMethod]
		public void Add_availability_invalid_price_currency()
		{
			var calendar = new Calendar(1);
			var price = new Price(100, 10000);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.AddAvailability(new DateTime(2020, 3, 1), new DateTime(2020, 3, 3), price));
		}

		[TestMethod]
		public void Add_availability_invalid_from_to()
		{
			var calendar = new Calendar(1);
			var price = new Price(100, Currency.EUR.Id);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.AddAvailability(new DateTime(2020, 3, 3), new DateTime(2020, 3, 1), price));
		}

		[TestMethod]
		public void Block_dates_success()
		{
			var calendar = new Calendar(1);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.BlockDates(new DateTime(2020, 3, 3), new DateTime(2020, 3, 1)));
		}

		[TestMethod]
		public void Block_dates_invalid_from_to()
		{
			var calendar = new Calendar(1);

			Assert.ThrowsException<FantasticStayDomainException>(() =>
				calendar.BlockDates(new DateTime(2020, 3, 3), new DateTime(2020, 3, 1)));
		}


		[TestMethod]
		public void Get_availability_valid()
		{
			var calendar = new Calendar(1);
			var price = new Price(100, Currency.EUR.Id);

			calendar.AddAvailability(new DateTime(2020, 3, 1), new DateTime(2020, 3, 3), price);
			calendar.BlockDates(new DateTime(2020, 3, 2), new DateTime(2020, 3, 2));

			var actualResult = calendar.GetAvailability(new DateTime(2020, 2, 1), new DateTime(2020, 4, 1)).ToList();
			var expectedResult = new Dictionary<DateTime, Price>
			{
				{ new DateTime(2020, 3, 1), price },
				{ new DateTime(2020, 3, 3), price }
			};

			CollectionAssert.AreEqual(expectedResult, actualResult);
		}

	}
}
