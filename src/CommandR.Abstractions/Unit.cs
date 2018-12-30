namespace CommandR.Abstractions
{
	/// <summary>
	/// Unit represents the return type of VOID. See <see cref="Command" and cref="Command{TResponse}"/>.
	/// </summary>
	public struct Unit
	{
		public static Unit Value => new Unit();
	}
}