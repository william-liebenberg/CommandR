using System;
using System.Collections;
using System.Collections.Generic;

namespace CommandR.UnitTests
{
	// TODO: Move this into some sort of "Common" or "Library" project
	public static class CollectionExtensions
	{
		public static IReadOnlyCollection<T> ToReadOnly<T>(this ICollection<T> source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			return source as IReadOnlyCollection<T> ?? new ReadOnlyCollectionAdapter<T>(source);
		}

		private sealed class ReadOnlyCollectionAdapter<T> : IReadOnlyCollection<T>
		{
			private readonly ICollection<T> _source;
			public ReadOnlyCollectionAdapter(ICollection<T> source) => this._source = source;
			public int Count => _source.Count;
			public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}