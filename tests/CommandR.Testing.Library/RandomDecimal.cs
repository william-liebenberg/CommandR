namespace CommandR.Testing.Library
{
	public class RandomDecimal
	{
		private readonly decimal _min = decimal.MinValue;
		private readonly decimal _max = decimal.MaxValue;

		public RandomDecimal()
		{
		}

		public RandomDecimal(decimal max)
		{
			_max = max;
		}

		public RandomDecimal(decimal min, decimal max)
		{
			_max = max;
			_min = min;
		}

		private decimal Value()
		{
			double r = new RandomDouble((double) _min, (double) _max);
			return (decimal)r;
		}

		public static implicit operator decimal(RandomDecimal r)
		{
			return r.Value();
		}
	}
}