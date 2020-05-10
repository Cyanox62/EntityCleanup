using System.Collections.Generic;
using MEC;
using Mirror;
using UnityEngine;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static IEnumerator<float> HandleDroppedItem(Pickup item)
		{
			yield return Timing.WaitForSeconds(Config.itemCleanupInterval);
			if (item != null) item.Delete();
		}

		public static IEnumerator<float> HandleRagdoll(GameObject ragdoll)
		{
			yield return Timing.WaitForSeconds(Config.ragdollCleanupInterval);
			if (ragdoll != null) NetworkServer.Destroy(ragdoll);
		}
	}
}
