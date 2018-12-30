namespace CommandR.UnitTests
{
	public class MovieCreatedEvent : Event
	{
		public MovieCreatedEvent(int movieId, string movieName)
		{
			MovieId = movieId;
			MovieName = movieName;
		}

		public int MovieId { get; set; }
		public string MovieName { get; set; }
	}
}