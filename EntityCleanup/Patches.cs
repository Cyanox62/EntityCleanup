using Exiled.API.Features.Items;
using HarmonyLib;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using MEC;
using Mirror;
using System;
using UnityEngine;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(Ragdoll), nameof(Ragdoll.ServerSpawnRagdoll))]
	class Patches
	{
		public static bool Prefix(global::ReferenceHub hub, PlayerStatsSystem.DamageHandlerBase handler)
		{
			if (!NetworkServer.active || hub == null)
			{
				return false;
			}
			GameObject model_ragdoll = hub.characterClassManager.CurRole.model_ragdoll;
			if (model_ragdoll == null || !GameObject.Instantiate<GameObject>(model_ragdoll).TryGetComponent<global::Ragdoll>(out Ragdoll ragdoll))
			{
				return false;
			}
			ragdoll.NetworkInfo = new global::RagdollInfo(hub, handler, model_ragdoll.transform.localPosition, model_ragdoll.transform.localRotation);
			NetworkServer.Spawn(ragdoll.gameObject);
			EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleRagdoll(ragdoll.gameObject)));
			return false;
		}
	}

	[HarmonyPatch(typeof(InventoryExtensions), nameof(InventoryExtensions.ServerCreatePickup))]
	class PickupPatch
	{
		public static bool Prefix(ref ItemPickupBase __result, Inventory inv, ItemBase item, PickupSyncInfo psi, bool spawn = true)
		{
			if (!NetworkServer.active)
			{
				throw new InvalidOperationException("Method ServerCreatePickup can only be executed on the server.");
			}
			ItemPickupBase itemPickupBase = GameObject.Instantiate<InventorySystem.Items.Pickups.ItemPickupBase>(item.PickupDropModel, inv.transform.position, global::ReferenceHub.GetHub(inv.gameObject).PlayerCameraReference.rotation * item.PickupDropModel.transform.rotation);
			itemPickupBase.NetworkInfo = psi;
			if (spawn)
			{
				NetworkServer.Spawn(itemPickupBase.gameObject);
				EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleDroppedItem(itemPickupBase)));
			}
			itemPickupBase.InfoReceived(default(PickupSyncInfo), psi);
			__result = itemPickupBase;
			return false;
		}
	}
}
