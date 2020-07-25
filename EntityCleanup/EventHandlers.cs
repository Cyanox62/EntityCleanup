using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

		public void OnRoundRestart()
		{
			Timing.KillCoroutines(coroutines);
			coroutines.Clear();
		}

		public void OnDroppedItem(ItemDroppedEventArgs ev)
		{
			coroutines.Add(Timing.RunCoroutine(HandleDroppedItem(ev.Pickup)));
		}
	}
}
