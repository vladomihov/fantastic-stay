using System;

namespace FantasticStay.Domain.Exceptions
{
	/// <summary>
	/// Exception type for domain exceptions
	/// </summary>
	public class FantasticStayDomainException : Exception
	{
		public FantasticStayDomainException()
		{ }

		public FantasticStayDomainException(string message)
			: base(message)
		{ }

		public FantasticStayDomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
