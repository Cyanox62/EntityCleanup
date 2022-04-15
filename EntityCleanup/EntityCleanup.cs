using Exiled.API.Features;
using HarmonyLib;

namespace EntityCleanup
{
    public class EntityCleanup : Plugin<Config>
    {
        internal static EntityCleanup instance;
        private Harmony hInstance;

        private EventHandlers ev;

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;

            instance = this;

            hInstance = new Harmony("cyanox.entitycleanup");
            hInstance.PatchAll();

            ev = new EventHandlers();

            Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;
            Exiled.Events.Handlers.Player.SpawningRagdoll += ev.OnSpawningRagdoll;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= ev.OnRoundRestart;
            Exiled.Events.Handlers.Player.SpawningRagdoll -= ev.OnSpawningRagdoll;

            hInstance.UnpatchAll();

            ev = null;
        }

        public override string Name => "EntityCleanup";
        public override string Author => "Cyanox";
    }
}
