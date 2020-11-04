using HarmonyLib;
using MEC;
using Mirror;
using UnityEngine;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(RagdollManager), "SpawnRagdoll")]
	class RagdollPatch
	{
		public static bool Prefix(RagdollManager __instance, Vector3 pos, Quaternion rot, Vector3 velocity, int classId, global::PlayerStats.HitInfo ragdollInfo, bool allowRecall, string ownerID, string ownerNick, int playerId)
		{
			Role role = __instance.hub.characterClassManager.Classes.SafeGet(classId);
			if (role.model_ragdoll == null) return false;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(role.model_ragdoll, pos + role.ragdoll_offset.position, Quaternion.Euler(rot.eulerAngles + role.ragdoll_offset.rotation));
			NetworkServer.Spawn(gameObject);
			Ragdoll component = gameObject.GetComponent<Ragdoll>();
			component.Networkowner = new Ragdoll.Info(ownerID, ownerNick, ragdollInfo, role, playerId);
			component.NetworkallowRecall = allowRecall;
			component.NetworkPlayerVelo = velocity;
			EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleRagdoll(gameObject)));

			return false;
        }
	}
}
