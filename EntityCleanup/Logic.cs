using System.Collections.Generic;
using MEC;
using Mirror;
using UnityEngine;
using InventorySystem.Items.Pickups;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static IEnumerator<float> HandleDroppedItem(ItemPickupBase item)
		{
			if (EntityCleanup.instance.Config.CleanupItems.Contains((int)item.Info.ItemId))
			{
				yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.ItemCleanupInterval);
				if (item != null) NetworkServer.Destroy(item.gameObject);
			}
		}

		public static IEnumerator<float> HandleRagdoll(GameObject ragdoll)
		{
			Exiled.API.Features.Log.Warn("handling ragdoll");
			yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.RagdollCleanupInterval);
			if (ragdoll != null) NetworkServer.Destroy(ragdoll);
		}
	}
}
