using Exiled.API.Interfaces;

namespace EntityCleanup
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool cleanRagdolls { get; set; } = true;
        public bool cleanPickups { get; set; } = true;
        public bool cleanupAfterNuke { get; set; } = true;
        public bool cleanupAfterDecont { get; set; } = true;
    }
}
