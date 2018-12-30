using System;
using System.Threading;

namespace CommandR.Testing.Library
{
	public class RandomDouble
	{
		private static int _seed = Environment.TickCount;

		private readonly double _min = double.MinValue;
		private readonly double _max = double.MaxValue;

		private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

		public RandomDouble()
		{
		}

		public RandomDouble(double max)
		{
			_max = max;
		}

		public RandomDouble(double min, double max)
		{
			_max = max;
			_min = min;
		}

		private double Value()
		{
			double range = _max - _min;
			return _min + (Rng.Value.NextDouble() * range);
		}

		public static implicit operator double(RandomDouble r)
		{
			return r.Value();
		}
	}
}