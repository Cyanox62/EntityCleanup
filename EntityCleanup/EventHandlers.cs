using MEC;
using System.Collections.Generic;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

		public void OnRoundRestart()
		{
			Timing.KillCoroutines(coroutines.ToArray());
			coroutines.Clear();
		}
	}
}
