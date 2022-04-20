using Exiled.API.Features;

namespace EntityCleanup
{
    public class EntityCleanup : Plugin<Config>
    {
        internal static EntityCleanup instance;
        //private Harmony hInstance;

        private EventHandlers ev;

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;

            instance = this;

            //hInstance = new Harmony("cyanox.entitycleanup");
            //hInstance.PatchAll();

            ev = new EventHandlers();

            Exiled.Events.Handlers.Warhead.Detonated += ev.OnNuke;
            Exiled.Events.Handlers.Map.Decontaminating += ev.OnDecontamination;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Warhead.Detonated -= ev.OnNuke;
            Exiled.Events.Handlers.Map.Decontaminating -= ev.OnDecontamination;

            //hInstance.UnpatchAll();

            ev = null;
        }

        public override string Name => "EntityCleanup";
        public override string Author => "Cyanox";
    }
}
