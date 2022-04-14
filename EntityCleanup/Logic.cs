using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using Mirror;
using UnityEngine;
using System;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Pickups;

namespace EntityCleanup
{
	partial class EventHandlers
	{
		public static IEnumerator<float> HandleDroppedItem(ItemPickupBase item)
		{
			if (EntityCleanup.instance.Config.IgnoreItems.Contains((int)item.Info.ItemId)) yield break;
			yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.ItemCleanupInterval);
			if (item != null) NetworkServer.Destroy(item.gameObject);
		}

		public static IEnumerator<float> HandleRagdoll(GameObject ragdoll)
		{
			yield return Timing.WaitForSeconds(EntityCleanup.instance.Config.RagdollCleanupInterval);
			if (ragdoll != null) NetworkServer.Destroy(ragdoll);
		}
	}
}
