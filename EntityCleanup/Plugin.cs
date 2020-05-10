using EXILED;
using Harmony;

namespace EntityCleanup
{
    public class Plugin : EXILED.Plugin
    {
        private EventHandlers ev;

        public override void OnEnable()
        {
            HarmonyInstance.Create("cyanox.entitycleanup").PatchAll();

            ev = new EventHandlers();

            Events.ItemDroppedEvent += ev.OnDroppedItem;
            Events.WaitingForPlayersEvent += ev.OnWaitingForPlayers;
            Events.RoundRestartEvent += ev.OnRoundRestart;
            Events.PlayerDeathEvent += ev.OnPlayerDie;
        }

        public override void OnDisable() { }

        public override void OnReload() { }

        public override string getName { get; } = "EntityCleanup";
    }
}
