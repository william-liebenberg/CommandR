using System;
using System.Threading;

namespace CommandR.Testing.Library
{
	public class RandomInt
	{
		private static int _seed = Environment.TickCount;

		private readonly int _min = int.MinValue;
		private readonly int _max = int.MaxValue;

		private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

		public RandomInt()
		{
		}

		public RandomInt(int max)
		{
			_max = max;
		}

		public RandomInt(int min, int max)
		{
			_max = max;
			_min = min;
		}

		public int Value()
		{
			return Rng.Value.Next(_min, _max);
		}

		public static implicit operator int(RandomInt r)
		{
			return r.Value();
		}
	}
}