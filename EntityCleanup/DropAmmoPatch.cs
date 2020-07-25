using Exiled.API.Features;
using HarmonyLib;
using MEC;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(AmmoBox), "DropAll")]
	class DropAmmoPatch
	{
		public static bool Prefix(AmmoBox __instance)
		{
			if (!EventHandlers.canDrop) return false;

			Player player = Player.Get(__instance.gameObject);
			for (int i = 0; i < __instance.amount.Count; i++)
			{
				uint num = __instance.amount[i];
				if (num != 0U)
				{
					__instance.amount[i] = 0U;
					EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleDroppedItem(__instance._inv.SetPickup(__instance.types[i].inventoryID, num, player.Position, __instance._inv.camera.transform.rotation, 0, 0, 0))));
				}
			}

			return false;
		}
	}
}
