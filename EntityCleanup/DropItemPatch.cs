using Harmony;
using MEC;
using Mirror;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(Inventory), "ServerDropAll")]
	class DropItemPatch
	{
		public static bool Prefix(Inventory __instance)
		{
			foreach (Inventory.SyncItemInfo syncItemInfo in (SyncList<Inventory.SyncItemInfo>)__instance.items)
				EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleDroppedItem(__instance.SetPickup(syncItemInfo.id, syncItemInfo.durability, __instance.transform.position, __instance.camera.transform.rotation, syncItemInfo.modSight, syncItemInfo.modBarrel, syncItemInfo.modOther))));
			for (byte index = 0; index < (byte)3; ++index)
			{
				if (__instance._ab.GetAmmo((int)index) != 0)
					EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleDroppedItem(__instance.SetPickup(__instance._ab.types[(int)index].inventoryID, (float)__instance._ab.GetAmmo((int)index), __instance.transform.position, __instance.camera.transform.rotation, 0, 0, 0)));
			}
			__instance.items.Clear();
			__instance._ab.Networkamount = "0:0:0";

			return false;
		}
	}
}
