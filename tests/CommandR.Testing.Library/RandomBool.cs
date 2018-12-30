namespace CommandR.Testing.Library
{
	public class RandomBool
	{
		public RandomBool()
		{
			
		}

		private static bool Value()
		{
			int r = new RandomInt(0, 100);
			return r >= 50;
		}

		public static implicit operator bool(RandomBool r)
		{
			return Value();
		}
	}
}