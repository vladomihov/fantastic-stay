using System.Collections.Generic;
using System.Linq;

namespace FantasticStay.Domain.SeedWork
{
	public abstract class ValueObject
	{
		public static bool operator ==(ValueObject left, ValueObject right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ValueObject left, ValueObject right)
		{
			return !Equals(left, right);
		}

		protected abstract IEnumerable<object> GetAtomicValues();

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObject)obj;
			var thisValues = GetAtomicValues();
			var otherValues = other.GetAtomicValues();

			return thisValues.SequenceEqual(otherValues);
		}

		public override int GetHashCode()
		{
			return GetAtomicValues()
				.Select(x => x?.GetHashCode() ?? 0)
				.Aggregate((x, y) => x ^ y);
		}

		public ValueObject GetCopy()
		{
			return this.MemberwiseClone() as ValueObject;
		}
	}

}
