using Harmony;
using MEC;
using Mirror;
using UnityEngine;

namespace EntityCleanup
{
	[HarmonyPatch(typeof(RagdollManager), "SpawnRagdoll")]
	class RagdollPatch
	{
		public static bool Prefix(RagdollManager __instance, Vector3 pos, Quaternion rot, int classId, PlayerStats.HitInfo ragdollInfo, bool allowRecall, string ownerID, string ownerNick, int playerId)
		{
            Role c = __instance.ccm.Classes.SafeGet(classId);
            if ((UnityEngine.Object)c.model_ragdoll != (UnityEngine.Object)null)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(c.model_ragdoll, pos + c.ragdoll_offset.position, Quaternion.Euler(rot.eulerAngles + c.ragdoll_offset.rotation));
                NetworkServer.Spawn(gameObject);
                gameObject.GetComponent<Ragdoll>().Networkowner = new Ragdoll.Info(ownerID, ownerNick, ragdollInfo, c, playerId);
                gameObject.GetComponent<Ragdoll>().NetworkallowRecall = allowRecall;
                EventHandlers.coroutines.Add(Timing.RunCoroutine(EventHandlers.HandleRagdoll(gameObject)));
            }
            if (ragdollInfo.GetDamageType().isScp || ragdollInfo.GetDamageType() == DamageTypes.Pocket)
            {
                __instance.RegisterScpFrag();
            }
            else
            {
                if (ragdollInfo.GetDamageType() == DamageTypes.Grenade)
                {
                    ++RoundSummary.kills_by_frag;
                }
            }

            return false;
        }
	}
}
