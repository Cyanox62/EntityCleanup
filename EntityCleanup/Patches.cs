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
