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
			if (EntityCleanup.instance.Config.IgnoreItems.Contains((int)item.ItemId)) yield break;
			yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.ItemCleanupInterval);
			if (item != null) item.Delete();
		}

		public static IEnumerator<float> HandleRagdoll(GameObject ragdoll)
		{
			yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.RagdollCleanupInterval);
			if (ragdoll != null) NetworkServer.Destroy(ragdoll);
		}
	}
}
