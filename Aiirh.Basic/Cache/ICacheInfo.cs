using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Cache {
	public interface ICacheInfo
	{
		string Name { get; }
		int CreateNr { get; }
		int TotalCallCount { get; }
		int CacheMissCallCount { get; }
		int CacheHitCallCount { get; }
		int CacheHitRatio { get; }
		int CacheRemovalExecutions { get; }
		int Count { get; }
		TimeSpan CacheTime { get; }

		/// <summary>
		///   Clears the cache
		/// </summary>
		void Clear();

		void ClearByKeySearch(List<string> keySearch);
	}
}