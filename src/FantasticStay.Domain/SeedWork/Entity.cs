namespace FantasticStay.Domain.SeedWork
{
	public abstract class Entity
	{
		public virtual int Id { get; protected set; }

		public bool IsTransient() => Id == default;

		public override bool Equals(object obj)
		{
			if (!(obj is Entity))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (GetType() != obj.GetType())
			{
				return false;
			}

			var item = (Entity)obj;
			if (IsTransient() || item.IsTransient())
			{
				return false;
			}

			return Id == item.Id;
		}

		public override int GetHashCode()
		{
			return IsTransient()
				? base.GetHashCode()
				: Id.GetHashCode();
		}
	}

}
