using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Kururu.Framework.Cache
{
	public class CacheManger<T>
	{
		private ConcurrentDictionary<string, T> _dictionary;

		public CacheManger ()
		{
			_dictionary = new ConcurrentDictionary<string, T> ();
		}

		public async Task<bool> ExistAsync (string Key)
		{
			await Task.Yield ();
			return _dictionary.ContainsKey (Key);
		}

		public async Task<T> GetAsync (string Key)
		{
			await Task.Yield ();
			if (await ExistAsync (Key))
			{
				return _dictionary [Key];
			}
			return default (T);
		}

		public async Task<bool> AddAsync (string Key, T Value)
		{
			await Task.Yield();
			return _dictionary.TryAdd(Key, Value);
		}

		public async Task<int> CountAsync ()
		{
			await Task.Yield ();
			return _dictionary.Count;
		}



	}
}
