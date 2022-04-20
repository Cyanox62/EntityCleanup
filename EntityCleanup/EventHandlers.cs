using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using System.Linq;

namespace EntityCleanup
{
    partial class EventHandlers
    {

        internal void OnNuke()
        {
            if (!EntityCleanup.instance.Config.cleanupAfterNuke) return;
            List<Exiled.API.Features.Ragdoll> delRag = new List<Exiled.API.Features.Ragdoll>();
            foreach (Exiled.API.Features.Ragdoll p in Map.Ragdolls)
            {
                if (ClosestRoom(p.Position).Zone != Exiled.API.Enums.ZoneType.Surface && EntityCleanup.instance.Config.cleanRagdolls) delRag.Add(p);
            }
            foreach (Exiled.API.Features.Ragdoll p in delRag)
            {
                //Log.Info("Removing ragdoll: " + ClosestRoom(p.Position).Zone);
                p.Delete();
            }
            delRag.Clear();

            List<Pickup> del = new List<Pickup>();
            foreach (Pickup p in Map.Pickups.ToList())
            {
                if (ClosestRoom(p.Position).Zone != Exiled.API.Enums.ZoneType.Surface && EntityCleanup.instance.Config.cleanPickups) del.Add(p);
            }
            foreach (Pickup p in del)
            {
                //Log.Info(p.Type + " - " + ClosestRoom(p.Position).Zone);
                p.Destroy();
            }
            del.Clear();
        }

        internal void OnDecontamination(DecontaminatingEventArgs ev)
        {
            if (!EntityCleanup.instance.Config.cleanupAfterDecont) return;
            List<Exiled.API.Features.Ragdoll> delRag = new List<Exiled.API.Features.Ragdoll>();
            foreach (Exiled.API.Features.Ragdoll p in Map.Ragdolls)
            {
                if (ClosestRoom(p.Position).Zone == Exiled.API.Enums.ZoneType.LightContainment && EntityCleanup.instance.Config.cleanRagdolls) delRag.Add(p);
            }
            foreach (Exiled.API.Features.Ragdoll p in delRag)
            {
                //Log.Info("Removing ragdoll: " + ClosestRoom(p.Position).Zone);
                p.Delete();
            }
            delRag.Clear();

            List<Pickup> del = new List<Pickup>();
            foreach (Pickup p in Map.Pickups.ToList())
            {
                if (ClosestRoom(p.Position).Zone == Exiled.API.Enums.ZoneType.LightContainment && EntityCleanup.instance.Config.cleanPickups) del.Add(p);
            }
            foreach (Pickup p in del)
            {
                //Log.Info(p.Type + " - " + ClosestRoom(p.Position).Zone);
                p.Destroy();
            }
            del.Clear();
        }

        internal Room ClosestRoom(UnityEngine.Vector3 pos)
        {
            Room closest = null;
            foreach (Room r in Room.List)
            {
                if (closest == null)
                {
                    closest = r;
                    continue;
                }
                if ((r.Position - pos).magnitude < (closest.Position - pos).magnitude) closest = r;
            }
            return closest;
        }
    }
}
