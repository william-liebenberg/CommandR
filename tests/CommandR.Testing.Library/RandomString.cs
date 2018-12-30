using System;

namespace CommandR.Testing.Library
{
	public class RandomString
	{
		private const string AllowedChars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public RandomString() : this(64)
		{
		}

		public RandomString(int len)
		{
			_lazyString = new Lazy<string>(() =>
			{
				var randomString = "";
				var curr = 0;
				while (curr < len)
				{
					randomString += AllowedChars[new RandomInt(0, AllowedChars.Length - 1)];
					curr++;
				}
				return randomString;
			});
		}

		private readonly Lazy<string> _lazyString;

		public static implicit operator string(RandomString r)
		{
			return r._lazyString.Value;
		}
	}
}