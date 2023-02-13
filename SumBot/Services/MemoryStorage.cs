using SumBot.Models;
using System.Collections.Concurrent;

namespace SumBot.Services
{
	public class MemoryStorage : IStorage
	{
		private readonly ConcurrentDictionary<long, Session> _sessions;

		public MemoryStorage()
		{
            _sessions = new ConcurrentDictionary<long, Session>();
        }

		public Session GetSession(long chatID)
		{
			// Возвращаем сессию по ключу, если она существует
			if (_sessions.ContainsKey(chatID))
				return _sessions[chatID];

			// Создаем и возвращаем новую сессию, если таковой не было

			var newSession = new Session() { SumType = "text" };
			_sessions.TryAdd(chatID, newSession);
			return newSession;
        }
    }
}