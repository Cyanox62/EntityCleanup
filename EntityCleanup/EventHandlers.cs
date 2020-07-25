using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();
		internal static bool canDrop = true;

		public void OnRoundRestart()
		{
			Timing.KillCoroutines(coroutines);
			coroutines.Clear();
			canDrop = false;
		}

		public void OnDroppedItem(ItemDroppedEventArgs ev)
		{
			coroutines.Add(Timing.RunCoroutine(HandleDroppedItem(ev.Pickup)));
		}

		public void OnWaitingForPlayers()
		{
			canDrop = true;
		}
	}
}
