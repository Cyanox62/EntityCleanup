using EXILED;
using MEC;
using System.Collections.Generic;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

		public void OnWaitingForPlayers()
		{
			Config.Reload();
		}

		public void OnRoundRestart()
		{
			Timing.KillCoroutines(coroutines);
			coroutines.Clear();
		}

		public void OnDroppedItem(ItemDroppedEvent ev)
		{
			coroutines.Add(Timing.RunCoroutine(HandleDroppedItem(ev.Item)));
		}
	}
}
