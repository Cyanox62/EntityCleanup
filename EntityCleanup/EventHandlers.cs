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
			Timing.KillCoroutines(coroutines.ToArray());
			coroutines.Clear();
		}

		internal void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
		{
			ev.IsAllowed = false;
			coroutines.Add(Timing.RunCoroutine(HandleRagdoll(new Exiled.API.Features.Ragdoll(ev.Info, true).GameObject)));
		}
	}
}
