using Exiled.API.Features;
using HarmonyLib;
using MEC;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(Inventory), "ServerDropAll")]
	class DropItemPatch
	{
		public static bool Prefix(Inventory __instance)
		{
			Player player = Player.Get(__instance.gameObject);
			foreach (global::Inventory.SyncItemInfo syncItemInfo in __instance.items)
			{
				EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleDroppedItem(__instance.SetPickup(syncItemInfo.id, syncItemInfo.durability, player.Position, __instance.camera.transform.rotation, syncItemInfo.modSight, syncItemInfo.modBarrel, syncItemInfo.modOther))));
			}
			__instance._ab.DropAll();
			__instance.items.Clear();

			return false;
		}
	}
}
