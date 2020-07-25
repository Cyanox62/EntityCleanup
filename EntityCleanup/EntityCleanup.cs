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

            Exiled.Events.Handlers.Server.WaitingForPlayers += ev.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ItemDropped += ev.OnDroppedItem;
            Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= ev.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ItemDropped -= ev.OnDroppedItem;
            Exiled.Events.Handlers.Server.RestartingRound -= ev.OnRoundRestart;

            hInstance.UnpatchAll();

            ev = null;
        }

        public override string Name => "EntityCleanup";
    }
}
